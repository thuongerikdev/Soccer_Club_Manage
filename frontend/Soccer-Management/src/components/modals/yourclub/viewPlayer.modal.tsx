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
    isOwner: boolean | string | undefined;  // Thêm isOwner vào props
    onLineupCreated: () => void; // Change to function type
}

function ViewPlayer(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, player, setPlayer, isOwner , onLineupCreated } = props;

    const [playerID, setPlayerId] = useState<number>(0);
    const [playerName, setPlayerName] = useState<string>('');
    const [playerPosition, setPlayerPosition] = useState<string>('');
    const [clubID, setClubId] = useState<number>(0);
    const [playerAge, setPlayerAge] = useState<number>(0);
    const [shirtNumber, setShirtNumber] = useState<number>(0);
    const [playerImage, setPlayerImage] = useState<string>('');
    const [phoneNumber, setPhoneNumber] = useState<number>(0);
    const [playerStatus, setPlayerStatus] = useState<number>(0);
    const [leg, setLeg] = useState<string>('');
    const [height, setHeight] = useState<number>(0);
    const [weight, setWeight] = useState<number>(10);

    useEffect(() => {
        if (player) {
            setPlayerId(player.playerID);
            setPlayerName(player.playerName || '');
            setPlayerPosition(player.playerPosition || '');
            setClubId(player.clubID || 0);
            setPlayerAge(player.playerAge || 0);
            setShirtNumber(player.shirtnumber || 0);
            setPlayerImage(player.playerImage || '');
            setPhoneNumber(player.phoneNumber || 0);
            setPlayerStatus(player.playerStatus || 0);
            setLeg(player.leg || '');
            setHeight(player.height || 0);
            setWeight(player.weight || 10);
        }
    }, [player]);

    const handleSubmit = () => {
        const requestData = {
            playerID: playerID,
            playerName: playerName,
            playerPosition: playerPosition,
            clubID: clubID,
            playerAge: playerAge,
            shirtnumber: shirtNumber,
            playerImage: playerImage,
            phoneNumber: phoneNumber,
            playerStatus: playerStatus,
            leg: leg,
            height: height,
            weight: weight,
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
            handleCloseModal();
            mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getPlayersByClub/${clubID}`);
        })
        .catch(err => {
            toast.error("Error updating player");
            console.error("Error:", err);
        });
    };
    
    
    const handleDelete = () => {
        if (window.confirm("Are you sure you want to delete this player?")) {
            const requestData = {
                playerID: playerID,
                playerName: playerName,
                playerPosition: playerPosition,
                clubID: clubID,
                playerAge: playerAge,
                shirtnumber: shirtNumber,
                playerImage: playerImage,
                phoneNumber: phoneNumber,
                playerStatus: 2,
                leg: leg,
                height: height,
                weight: weight,
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
                mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getPlayersByClub/${clubID}`);
                handleCloseModal();
                
            })
            .catch(err => {
                toast.error("Error updating player");
                console.error("Error:", err);
            });
        };
    }
    

    const handleCloseModal = () => {
        setPlayer(null); // Reset state player
        setShowModalUpdate(false);
        setPlayerId(0);
        setPlayerName('');
        setPlayerPosition('');
        setPlayerAge(0);
        setShirtNumber(0);
        setPlayerImage('');
        setPhoneNumber(0);
        setPlayerStatus(0);
        setLeg('');
        setHeight(0);
        setWeight(10);
    };

    const isDisabled = !isOwner;

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
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Position</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player position" 
                        value={playerPosition}
                        onChange={(e) => setPlayerPosition(e.target.value)} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Club ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter club ID" 
                        value={clubID}
                        onChange={(e) => setClubId(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Age</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter age" 
                        value={playerAge}
                        onChange={(e) => setPlayerAge(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Shirt Number</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter shirt number" 
                        value={shirtNumber}
                        onChange={(e) => setShirtNumber(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Player Image</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player image URL" 
                        value={playerImage}
                        onChange={(e) => setPlayerImage(e.target.value)} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Phone Number</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter phone number" 
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Player Status</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player status" 
                        value={playerStatus}
                        onChange={(e) => setPlayerStatus(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Leg</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter player's dominant leg" 
                        value={leg}
                        onChange={(e) => setLeg(e.target.value)} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Height</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter height" 
                        value={height}
                        onChange={(e) => setHeight(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Weight</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter weight" 
                        value={weight}
                        onChange={(e) => setWeight(Number(e.target.value))} 
                        disabled={isDisabled} 
                    />
                </Form.Group>

            </Modal.Body>
            <Modal.Footer>
                {isOwner && (
                    <>
                     <Button variant="primary" onClick={handleSubmit}>Save</Button>
                     <Button variant="primary" onClick={handleDelete}>delete</Button>
                    </>
                   
                )}
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default ViewPlayer;
