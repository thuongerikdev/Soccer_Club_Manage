import { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalCreate: boolean;
    setShowModalCreate: (value: boolean) => void;
    clubID? : number

}

function CreatePlayerModal(props: IProps) {
    const { showModalCreate, setShowModalCreate  , clubID  } = props;

    // State for player attributes
    const [playerName, setPlayerName] = useState<string>('');
    const [playerPosition, setPlayerPosition] = useState<string>('');
    const [playerImage, setPlayerImage] = useState<string>('');
    const [playerAge, setPlayerAge] = useState<number | ''>(0);
    const [shirtNumber, setShirtNumber] = useState<number | ''>(0);
    const [playerStatus, setPlayerStatus] = useState<number | ''>(0);
    const [leg, setLeg] = useState<string>('');
    const [height, setHeight] = useState<number | ''>(0);
    const [weight, setWeight] = useState<number | ''>(0);

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setPlayerImage(reader.result as string); // Save Base64 string
            };
            reader.readAsDataURL(file);
        }
    };

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_PLAYER}/createPlayer`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "POST",
            body: JSON.stringify({
                playerName,
                playerPosition,
                playerImage :playerImage, // Base64 string
                clubID: clubID,
                playerAge,
                shirtnumber: shirtNumber,
                playerStatus,
                leg,
                height,
                weight
            }),
        })
        .then(res => {
            if (!res.ok) {
                throw new Error(`HTTP error! status: ${res.status}`);
            }
            return res.json();
        })
        .then(res => {
            toast.success("Create successful");
            handleCloseModal();
            // onLineupCreated(); // Refresh the lineup data
            mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getPlayerClub/${clubID}` );
        })
        .catch(err => {
            toast.error("Error creating player");
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        // Reset input fields
        setPlayerName('');
        setPlayerPosition('');
        setPlayerImage('');
        setPlayerAge(0);
        setShirtNumber(0);
        setPlayerStatus(0);
        setLeg('');
        setHeight(0);
        setWeight(0);
        setShowModalCreate(false);
    };

    return (
        <Modal
            show={showModalCreate}
            onHide={handleCloseModal}
            backdrop="static"
            keyboard={false}
            size='lg'
        >
            <Modal.Header closeButton>
                <Modal.Title>Create Player</Modal.Title>
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
                    <Form.Label>Image</Form.Label>
                    <Form.Control 
                        type="file" 
                        accept="image/*" 
                        onChange={handleImageChange} 
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
                        onChange={(e) => setPlayerStatus(e.target.value ? Number(e.target.value) : 0)} 
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
                    <Form.Label>Height (cm)</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter height" 
                        value={height === 0 ? '' : height}
                        onChange={(e) => setHeight(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Weight (kg)</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter weight" 
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

export default CreatePlayerModal;