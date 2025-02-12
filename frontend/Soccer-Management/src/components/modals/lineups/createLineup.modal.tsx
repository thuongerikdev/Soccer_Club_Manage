import { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalCreate: boolean;
    setShowModalCreate: (value: boolean) => void;
}

function CreateLineUpModal(props: IProps) {
    const { showModalCreate, setShowModalCreate } = props;

    const [lineUpName, setLineUpName] = useState<string>('');
    const [lineUpType, setLineUpType] = useState<string>('');
    const [playerNumber, setPlayerNumber] = useState<number | ''>(0);
    const [clubId, setClubId] = useState<number | ''>(0);

    const handleSubmit = async () => {
        // Input validation
        if (!lineUpName || !lineUpType || playerNumber === 0 || clubId === 0) {
            toast.error("All fields are required.");
            return;
        }

        const lineUpData = {
            clubID: Number(clubId),  // Corrected key name to match API format
            lineUpName,
            lineUpType,
            playerNumber: Number(playerNumber),  // Added playerNumber
        };
    
        console.log("Submitting lineup data:", lineUpData);
    
        try {
            const response = await fetch(`${process.env.NEXT_PUBLIC_LINEUP}/createLineUp`, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                method: "POST",
                body: JSON.stringify(lineUpData),
            });
    
            const res = await response.json();
    
            console.log("Server response:", res);
            toast.success("Create successful");
            handleCloseModal();
            mutate(`${process.env.NEXT_PUBLIC_LINEUP}/getAllLineUp`);
        } catch (err) {
            toast.error("Error creating lineup: " + err);
            console.error("Error details:", err);
        }
    };

    const handleCloseModal = () => {
        setLineUpName('');
        setLineUpType('');
        setPlayerNumber(0);
        setClubId(0);
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
                <Modal.Title>Create Lineup</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Lineup Name</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter lineup name" 
                        value={lineUpName}
                        onChange={(e) => setLineUpName(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Lineup Type</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter lineup type" 
                        value={lineUpType}
                        onChange={(e) => setLineUpType(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Player Number</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter player number" 
                        value={playerNumber === 0 ? '' : playerNumber}
                        onChange={(e) => setPlayerNumber(e.target.value ? Number(e.target.value) : 0)} 
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

export default CreateLineUpModal;
