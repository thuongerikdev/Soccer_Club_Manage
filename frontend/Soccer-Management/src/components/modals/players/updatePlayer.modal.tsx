import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';


interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    setPlayer: (value: IPlayer | null) => void;
    player: IPlayer | null;
}

function UpdatePlayerModal(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, player, setPlayer } = props;

    const [playerId, setPlayerId] = useState<number>(0);
    const [playerName, setPlayerName] = useState<string>('');
    const [playerPosition, setPlayerPosition] = useState<string>('');
    const [playerNationality, setPlayerNationality] = useState<string>('');
    const [playerImage, setPlayerImage] = useState<string>('');
    const [clubId, setClubId] = useState<number>(0);
    const [playerAge, setPlayerAge] = useState<number>(0);
    const [playerValue, setPlayerValue] = useState<number>(0.0); // double
    const [playerHealth, setPlayerHealth] = useState<number>(0);
    const [playerSkill, setPlayerSkill] = useState<number>(0);
    const [playerSalary, setPlayerSalary] = useState<number>(0.0); // double
    const [shirtNumber, setShirtNumber] = useState<number>(0);
    const [playerStatus, setPlayerStatus] = useState<number>(0);
    const [leg, setLeg] = useState<string>('');
    const [height, setHeight] = useState<number>(0.0); // double
    const [weight, setWeight] = useState<number>(0.0); // double

    useEffect(() => {
        if (player) {
            setPlayerId(player.playerId);
            setPlayerName(player.playerName || '');
            setPlayerPosition(player.playerPosition || '');
            setPlayerNationality(player.playerNationality || '');
            setPlayerImage(player.playerImage || '');
            setClubId(player.clubId || 0);
            setPlayerAge(player.playerAge || 0);
            setPlayerValue(player.playerValue || 0.0); // double
            setPlayerHealth(player.playerHealth || 0);
            setPlayerSkill(player.playerSkill || 0);
            setPlayerSalary(player.playerSalary || 0.0); // double
            setShirtNumber(player.shirtnumber || 0);
            setPlayerStatus(player.playerStatus || 0);
            setLeg(player.leg || '');
            setHeight(player.height || 0.0); // double
            setWeight(player.weight || 0.0); // double
        }
    }, [player]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_PLAYER}/update/${playerId}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify({
                PlayerId: playerId,
                PlayerName: playerName,
                PlayerPosition: playerPosition,
                PlayerNationality: playerNationality,
                PlayerImage: playerImage,
                ClubId: clubId,
                PlayerAge: playerAge,
                PlayerValue: playerValue,
                PlayerHealth: playerHealth,
                PlayerSkill: playerSkill,
                PlayerSalary: playerSalary,
                Shirtnumber: shirtNumber,
                PlayerStatus: playerStatus,
                leg: leg,
                height: height,
                weight: weight,
            }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success(res.em); // Adjust based on your API response
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getall`);
            }
        })
        .catch(err => {
            toast.error("Error updating player");
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        setPlayer(null); // Reset player state
        setShowModalUpdate(false);
        setPlayerId(0);
        setPlayerName('');
        setPlayerPosition('');
        setPlayerNationality('');
        setPlayerImage('');
        setClubId(0);
        setPlayerAge(0);
        setPlayerValue(0.0);
        setPlayerHealth(0);
        setPlayerSkill(0);
        setPlayerSalary(0.0);
        setShirtNumber(0);
        setPlayerStatus(0);
        setLeg('');
        setHeight(0.0);
        setWeight(0.0);
    };

    return (
        <Modal
            show={showModalUpdate}
            onHide={handleCloseModal}
            backdrop="static"
            keyboard={false}
            size='lg'
        >
            <Modal.Header closeButton>
                <Modal.Title>Update Player</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Player Name</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player name" 
                        value={playerName}
                        onChange={(e) => setPlayerName(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Position</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player position" 
                        value={playerPosition}
                        onChange={(e) => setPlayerPosition(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Nationality</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter nationality" 
                        value={playerNationality}
                        onChange={(e) => setPlayerNationality(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Image URL</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter image URL" 
                        value={playerImage}
                        onChange={(e) => setPlayerImage(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Club ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter club ID" 
                        value={clubId}
                        onChange={(e) => setClubId(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Age</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter age" 
                        value={playerAge}
                        onChange={(e) => setPlayerAge(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Value</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player value" 
                        value={playerValue}
                        onChange={(e) => setPlayerValue(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Health</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter health" 
                        value={playerHealth}
                        onChange={(e) => setPlayerHealth(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Skill</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter skill level" 
                        value={playerSkill}
                        onChange={(e) => setPlayerSkill(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Salary</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter salary" 
                        value={playerSalary}
                        onChange={(e) => setPlayerSalary(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Shirt Number</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter shirt number" 
                        value={shirtNumber}
                        onChange={(e) => setShirtNumber(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Status</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player status" 
                        value={playerStatus}
                        onChange={(e) => setPlayerStatus(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Leg</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter leg (e.g., left or right)" 
                        value={leg}
                        onChange={(e) => setLeg(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Height (cm)</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter height" 
                        value={height}
                        onChange={(e) => setHeight(Number(e.target.value))} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Weight (kg)</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter weight" 
                        value={weight}
                        onChange={(e) => setWeight(Number(e.target.value))} 
                    />
                </Form.Group>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>Save</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default UpdatePlayerModal;