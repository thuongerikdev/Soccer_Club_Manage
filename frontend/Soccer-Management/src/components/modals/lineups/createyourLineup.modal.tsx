import { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalCreate: boolean;
    setShowModalCreate: (value: boolean) => void;
    clubID?: number;
    onLineupCreated: () => void; // Change to function type
}

function CreateLineUpModal(props: IProps) {

    const { showModalCreate, setShowModalCreate, clubID, onLineupCreated } = props;
    const [lineUpName, setLineUpName] = useState<string>('');
    const [lineUpType, setLineUpType] = useState<string>('');
    const [matchType, setMatchType] = useState<string>('');



    const handleSubmit = async () => {
        // Input validation
        if (!lineUpName || !lineUpType || !matchType) {
            toast.error("All fields are required.");
            return;
        }

        const lineUpData = {
            clubId: Number(clubID),
            lineUpName,
            lineUpType,
            playerNumber: matchType,
        };

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
            toast.success("Create successful");
            onLineupCreated(); // Refresh the lineup data
            handleCloseModal();
            mutate(`${process.env.NEXT_PUBLIC_LINEUP}/getLineUpClub/${clubID}`);

            
            //  else {
            //     toast.error("Create failed: " + res.message);
            // }
        } catch (err) {
            toast.error("Error creating lineup: " + err);
        }
    };

    const handleCloseModal = () => {
        setLineUpName('');
        setLineUpType('');
        setMatchType('');
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
                        type="text" 
                        placeholder="Enter player number" 
                        value={matchType}
                        onChange={(e) => setMatchType(e.target.value)} 
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