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

function CreateModal(props: IProps) {
    const { showModalCreate, setShowModalCreate } = props;

    const [clubName, setClubName] = useState<string>('');
    const [clubDescription, setClubDescription] = useState<string>('');
    const [clubLogo, setClubLogo] = useState<string>('');
    const [clubBanner, setClubBanner] = useState<string>('');
    const [boss, setBoss] = useState<number | ''>(0);
    const [budget, setBudget] = useState<number | ''>(0);
    const [clubLevel, setClubLevel] = useState<string | ''>('');
    const [clubAge, setClubAge] = useState<number | ''>(0);

    const handleSubmit = () => {
        const clubData = {
            clubName,
            clubDescription,
            clubLogo,
            clubBanner,
            userId: boss, // Change 'boss' to 'userId'
            budget,
            clubLevel: Number(clubLevel), // Ensure clubLevel is a number
            clubAge: String(clubAge), // Ensure clubAge is a string
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
            console.log("Server response:", res); // Log the server response
            if (res) {
                toast.success("Create successful");
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_CLUB}/getall`);
            }
        })
        .catch(err => {
            toast.error("Error creating club: " + err.message); // Log the error message
            console.error("Error details:", err);
        });
    };
    const handleCloseModal = () => {
        setClubName('');
        setClubDescription('');
        setClubLogo('');
        setClubBanner('');
        setBoss(0);
        setBudget(0);
        setClubLevel('');
        setClubAge(0);
        setShowModalCreate(false); // Đóng modal sau khi hoàn tất
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
                    <Form.Label>Boss User ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter boss user ID" 
                        value={boss === 0 ? '' : boss}
                        onChange={(e) => setBoss(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Budget</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter budget" 
                        value={budget === 0 ? '' : budget}
                        onChange={(e) => setBudget(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Club Level</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter club level" 
                        value={clubLevel}
                        onChange={(e) => setClubLevel(e.target.value)} 
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