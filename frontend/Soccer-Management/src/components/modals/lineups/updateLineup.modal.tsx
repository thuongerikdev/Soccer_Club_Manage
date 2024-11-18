import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    setLineUp: (value: LineUp | null) => void;
    lineUp: LineUp | null;
}

function UpdateLineUpModal(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, lineUp, setLineUp } = props;

    const [lineUpID, setLineUpID] = useState<number>(0);
    const [lineUpName, setLineUpName] = useState<string>('');
    const [lineUpType, setLineUpType] = useState<string>('');
    const [playerNumber, setPlayerNumber] = useState<number>(0);
    const [createAt, setCreateAt] = useState<string>(new Date().toISOString());

    useEffect(() => {
        if (lineUp) {
            setLineUpID(lineUp.lineUpID);
            setLineUpName(lineUp.lineUpName || '');
            setLineUpType(lineUp.lineUpType || '');
            setPlayerNumber(lineUp.playerNumber || 0);
            setCreateAt(lineUp.createAt || new Date().toISOString());
        }
    }, [lineUp]);

    const handleSubmit = () => {
        // Input validation
        if (!lineUpName || !lineUpType || playerNumber === 0) {
            toast.error("All fields are required.");
            return;
        }

        const lineUpData = {
            lineUpID,
            lineUpName,
            lineUpType,
            playerNumber,
            createAt,
        };

        fetch(`${process.env.NEXT_PUBLIC_LINEUP}/updateLineUp`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify(lineUpData),
        })
        .then(res => {
            if (!res.ok) {
                // Check for any error in the response
                throw new Error(`HTTP error! Status: ${res.status}`);
            }
            return res.json(); // Parse response as JSON
        })
        .then(res => {
            if (res) {
                if (res.EM) {
                    toast.success(res.ErrorMessage); // Assuming `EM` is the success message
                }
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_LINEUP}/getAllLineUp`);
            }
        })
        .catch(err => {
            toast.error("Error updating lineup: " + err.message);
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        setLineUpName("");
        setLineUpType("");
        setPlayerNumber(0);
        setCreateAt(new Date().toISOString());
        setShowModalUpdate(false);
        setLineUp(null);
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
                <Modal.Title>Update Lineup</Modal.Title>
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

export default UpdateLineUpModal;
