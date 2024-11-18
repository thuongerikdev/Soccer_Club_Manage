import { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalCreate: boolean;
    setShowModalCreate: (value: boolean) => void;
    userId: number; // Ensure this is defined as a number
}

function CreateModal({ showModalCreate, setShowModalCreate, userId }: IProps) {
    const [clubName, setClubName] = useState<string>('');
    const [clubDescription, setClubDescription] = useState<string>('');
    const [clubLogo, setClubLogo] = useState<string>('');
    const [clubBanner, setClubBanner] = useState<string>('');
    const [budget, setBudget] = useState<number | ''>(0);
    const [clubLevel, setClubLevel] = useState<number | ''>('');
    const [clubAge, setClubAge] = useState<number | ''>(0);

    const handleSubmit = () => {
        const clubData = {
            userID: userId,
            clubName,
            clubDescription,
            clubLogo,
            clubBanner,
            clubLevel: String(clubLevel),
            clubAge: String(clubAge),
        };

        console.log("Submitting club data:", clubData); // Log the data being sent

        fetch(`${process.env.NEXT_PUBLIC_CLUB}/create`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "POST",
            body: JSON.stringify(clubData),
        })
        .then(res => {
            if (!res.ok) {
                throw new Error(`HTTP error! Status: ${res.status}`);
            }
            return res.json();
        })
        .then(res => {
            console.log("Server response:", res);
            if (res) {
                toast.success("Create successful");
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_CLUB}/getall`);
            }
        })
        .catch(err => {
            toast.error("Error creating club: " + err.message);
            console.error("Error details:", err);
        });
    };

    const handleCloseModal = () => {
        setClubName('');
        setClubDescription('');
        setClubLogo('');
        setClubBanner('');
        setBudget(0);
        setClubLevel(0);
        setClubAge(0);
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
                <Modal.Title>Create Club</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {/* Form fields remain unchanged */}
                <Form.Group className="mb-3">
                    <Form.Label>Club Name</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter club name" 
                        value={clubName}
                        onChange={(e) => setClubName(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club Description</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter club description" 
                        value={clubDescription}
                        onChange={(e) => setClubDescription(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club Logo URL</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter club logo URL" 
                        value={clubLogo}
                        onChange={(e) => setClubLogo(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club Banner URL</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter club banner URL" 
                        value={clubBanner}
                        onChange={(e) => setClubBanner(e.target.value)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club Level</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter club level" 
                        value={clubLevel}
                        onChange={(e) => setClubLevel(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club Age</Form.Label>
                    <Form.Control 
                        type="number"
                        placeholder="Enter club age"
                        value={clubAge === 0 ? '' : clubAge}
                        onChange={(e) => setClubAge(e.target.value ? Number(e.target.value) : 0)} 
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

export default CreateModal;