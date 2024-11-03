import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    setPlayer: (value: IPlayer | null) => void; // Đổi từ setUser thành setPlayer
    player: IPlayer | null; // Đổi từ user thành player
}

function UpdatePlayerModal(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, player, setPlayer } = props;
    const [playerId, setPlayerId] = useState<number>(0);
    const [playerName, setPlayerName] = useState<string>('');
    const [playerPosition, setPlayerPosition] = useState<string>('');
    const [playerNationality, setPlayerNationality] = useState<string>('');
    const [playerImage, setPlayerImage] = useState<string>('');
    const [clubId, setClubId] = useState<number | ''>(0);
    const [playerAge, setPlayerAge] = useState<number | ''>(0);
    const [playerValue, setPlayerValue] = useState<number | ''>(0);
    const [playerHealth, setPlayerHealth] = useState<number | ''>(100);
    const [playerSkill, setPlayerSkill] = useState<number | ''>(50);
    const [playerSalary, setPlayerSalary] = useState<number | ''>(0);
    const [shirtNumber, setShirtNumber] = useState<number | ''>(0);
    const [playerStatus, setPlayerStatus] = useState<number | ''>(1);
    const [leg, setLeg] = useState<string>('');
    const [height, setHeight] = useState<number | ''>(0);
    const [weight, setWeight] = useState<number | ''>(0);

    useEffect(() => {
        if (player && player.PlayerId) {
            setPlayerId(player.PlayerId);
            setPlayerName(player.PlayerName || '');
            setPlayerPosition(player.PlayerPosition || '');
            setPlayerNationality(player.PlayerNationality || '');
            setPlayerImage(player.PlayerImage || '');
            setClubId(player.ClubId || 0);
            setPlayerAge(player.PlayerAge || 0);
            setPlayerValue(player.PlayerValue || 0);
            setPlayerHealth(player.PlayerHealth || 100);
            setPlayerSkill(player.PlayerSkill || 50);
            setPlayerSalary(player.PlayerSalary || 0);
            setShirtNumber(player.Shirtnumber || 0);
            setPlayerStatus(player.PlayerStatus || 1);
            setLeg(player.leg || '');
            setHeight(player.height || 0);
            setWeight(player.weight || 0);
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
                PlayerName: playerName,
                PlayerPosition: playerPosition,
                PlayerNationality: playerNationality,
                PlayerImage: playerImage,
                PlayerAge: playerAge,
                PlayerValue: playerValue,
                PlayerHealth: playerHealth,
                PlayerSkill: playerSkill,
                PlayerSalary: playerSalary,
                PlayerStatus: playerStatus,
                Shirtnumber: shirtNumber,
                ClubId: clubId,
                height: height, // Ensure this matches your API expectations
                weight: weight, // Ensure this matches your API expectations
                leg: leg,       // Ensure this matches your API expectations
            }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success(res.em); // Ensure res.em contains the expected success message
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
        setPlayerName("");
        setPlayerPosition("");
        setPlayerNationality("");
        setPlayerImage("");
        setClubId(0);
        setPlayerAge(0);
        setPlayerValue(0);
        setPlayerHealth(100);
        setPlayerSkill(50);
        setPlayerSalary(0);
        setShirtNumber(0);
        setPlayerStatus(1);
        setLeg("");
        setHeight(0);
        setWeight(0);
        setShowModalUpdate(false);
        setPlayer(null);
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
                        placeholder="Enter player nationality" 
                        value={playerNationality}
                        onChange={(e) => setPlayerNationality(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Image URL</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player image URL" 
                        value={playerImage}
                        onChange={(e) => setPlayerImage(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Club ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter club ID" 
                        value={clubId === 0 ? '' : clubId}
                        onChange={(e) => setClubId(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Age</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player age" 
                        value={playerAge === 0 ? '' : playerAge}
                        onChange={(e) => setPlayerAge(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Value</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player value" 
                        value={playerValue === 0 ? '' : playerValue}
                        onChange={(e) => setPlayerValue(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Health</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player health" 
                        value={playerHealth === 0 ? '' : playerHealth}
                        onChange={(e) => setPlayerHealth(e.target.value ? Number(e.target.value) : 100)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Skill</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player skill" 
                        value={playerSkill === 0 ? '' : playerSkill}
                        onChange={(e) => setPlayerSkill(e.target.value ? Number(e.target.value) : 50)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Salary</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player salary" 
                        value={playerSalary === 0 ? '' : playerSalary}
                        onChange={(e) => setPlayerSalary(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Shirt Number</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter shirt number" 
                        value={shirtNumber === 0 ? '' : shirtNumber}
                        onChange={(e) => setShirtNumber(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Status</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player status" 
                        value={playerStatus === 0 ? '' : playerStatus}
                        onChange={(e) => setPlayerStatus(e.target.value ? Number(e.target.value) : 1)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Leg</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter leg (left/right)" 
                        value={leg}
                        onChange={(e) => setLeg(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Height</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter height in cm" 
                        value={height === 0 ? '' : height}
                        onChange={(e) => setHeight(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Weight</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter weight in kg" 
                        value={weight === 0 ? '' : weight}
                        onChange={(e) => setWeight(e.target.value ? Number(e.target.value) : 0)} 
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