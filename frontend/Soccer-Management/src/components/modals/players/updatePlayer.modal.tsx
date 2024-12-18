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
    const [playerImage, setPlayerImage] = useState<string>('');
    const [clubId, setClubId] = useState<number>(0);
    const [playerAge, setPlayerAge] = useState<number>(0);
    const [shirtNumber, setShirtNumber] = useState<number>(0);
    const [playerStatus, setPlayerStatus] = useState<number>(0);
    const [leg, setLeg] = useState<string>('');
    const [height, setHeight] = useState<number>(0.0); // double
    const [weight, setWeight] = useState<number>(0.0); // double
    const [phoneNumber, setPhoneNumber] = useState<number>(0); // New state for phone number

    useEffect(() => {
        if (player) {
            setPlayerId(player.playerID);
            setPlayerName(player.playerName || '');
            setPlayerPosition(player.playerPosition || '');
            setPlayerImage(player.playerImage || '');
            setClubId(player.clubID || 0);
            setPlayerAge(player.playerAge || 0);
            setShirtNumber(player.shirtnumber || 0);
            setPlayerStatus(player.playerStatus || 0);
            setLeg(player.leg || '');
            setHeight(player.height || 0.0);
            setWeight(player.weight || 0.0);
            setPhoneNumber(player.phoneNumber || 0); // Set phone number from player data
        }
    }, [player]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_PLAYER}/updatePlayer`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify({
                playerId: playerId,
                playerName: playerName,
                playerPosition: playerPosition,
                playerImage: playerImage,
                clubID: clubId,
                playerAge: playerAge,
                shirtnumber: shirtNumber,
                playerStatus: playerStatus,
                leg: leg,
                height: height,
                weight: weight,
                phoneNumber: phoneNumber, // Include phone number in the request
            }),
        })
        .then(res => {
            if (!res.ok) {
                throw new Error(`Request failed with status: ${res.status}`);
            }
            return res.json(); // Get response as JSON
        })
        .then(res => {
            toast.success(res.em); // Adjust based on your API response
            handleCloseModal();
            mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getall`);
        })
        .catch(err => {
            toast.error(`Error updating player: ${err.message}`);
            console.error("Error:", err);
        });
    };

    const handleCloseModal = () => {
        setPlayer(null); // Reset player state
        setShowModalUpdate(false);
        setPlayerId(0);
        setPlayerName('');
        setPlayerPosition('');
        setPlayerImage('');
        setClubId(0);
        setPlayerAge(0);
        setShirtNumber(0);
        setPlayerStatus(0);
        setLeg('');
        setHeight(0.0);
        setWeight(0.0);
        setPhoneNumber(0); // Reset phone number
    };
    const handleDelete = () => {
        if (window.confirm("Are you sure you want to delete this player?")) {
            const requestData = {
                playerId: playerId,
                playerName: playerName,
                playerPosition: playerPosition,
                playerImage: playerImage,
                clubID: clubId,
                playerAge: playerAge,
                shirtnumber: shirtNumber,
                playerStatus: 2,
                leg: leg,
                height: height,
                weight: weight,
                phoneNumber: phoneNumber, // Include phone number in the request
            };
        
            console.log("Request Payload:", requestData); // Log the request payload for debugging
        
            fetch(`${process.env.NEXT_PUBLIC_PLAYER}/updatePlayer`, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                method: "PUT",
                body: JSON.stringify(requestData),
            })
            .then(res => {
                if (!res.ok) {
                    throw new Error(`Failed with status ${res.status}`);
                }
                return res.json();
            })
            .then(res => {
                toast.success("Create successful");
                // onLineupCreated()
                mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getall`);
                handleCloseModal();
                
            })
            .catch(err => {
                toast.error("Error updating player");
                console.error("Error:", err);
            });
        };
    }
    

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
                    <Form.Label>Image URL</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter image URL" 
                        value={playerImage}
                        onChange={(e) => setPlayerImage(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Phone Number</Form.Label> {/* New field for phone number */}
                    <Form.Control 
                        type="text" 
                        placeholder="Enter phone number" 
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(Number(e.target.value))} 
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
            <Button variant="primary" onClick={handleDelete}>delete</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default UpdatePlayerModal;