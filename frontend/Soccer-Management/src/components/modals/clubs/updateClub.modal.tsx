import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';



interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    setClub: (value: IClub | null) => void;
    club: IClub | null;
}

function UpdateModals(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, club, setClub } = props;
    
    const [clubId, setClubId] = useState<number>(0);
    const [clubName, setClubName] = useState<string>('');
    const [clubDescription, setClubDescription] = useState<string>('');
    const [clubLogo, setClubLogo] = useState<string>('');
    const [clubBanner, setClubBanner] = useState<string>('');
    const [boss, setBoss] = useState<number | ''>(0);
    const [budget, setBudget] = useState<number | ''>(0);
    const [clubLevel, setClubLevel] = useState<number | 0>(0);
    const [clubAge, setClubAge] = useState<string | ''>('');

    useEffect(() => {
        if (club && club.clubId) {
            setClubId(club.clubId);
            setClubName(club.clubName || '');
            setClubDescription(club.clubDescription || '');
            setClubLogo(club.clubLogo || '');
            setClubBanner(club.clubBanner || '');
            setBoss(club.userId || '');
            setBudget(club.budget || 0.0);
            setClubLevel(club.clubLevel || 0.0);
            setClubAge(club.clubAge || '');
        }
    }, [club]);

    const handleSubmit = () => {
        // Ensure you have these fields defined in your component's state
        fetch(`${process.env.NEXT_PUBLIC_CLUB}/update/${clubId}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify({
                ClubId: clubId,               // Include ClubId
                ClubName: clubName,           // Map to expected API input
                ClubDescription: clubDescription,
                UserId: boss,               // Assume you have userId in your state
                ClubLogo: clubLogo,
                ClubBanner: clubBanner,
                Budget: budget,               // Ensure this is included
                // Add additional fields as needed
                ClubLevel: clubLevel,         // If applicable
                ClubAge: clubAge,             // If applicable
            }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success(res.EM); // Adjusted to match the expected response key
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_CLUB}/getall`);
            }
        })
        .catch(err => {
            toast.error("Error updating club");
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        setClubName("");
        setClubDescription("");
        setClubLogo("");
        setClubBanner("");
        setBoss(0);
        setBudget(0);
        setShowModalUpdate(false);
        setClub(null);
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
                <Modal.Title>Update Club</Modal.Title>
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
                        value={boss}
                        onChange={(e) => setBoss(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Budget</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter budget" 
                        value={budget}
                        onChange={(e) => setBudget(e.target.value ? Number(e.target.value) : 0)} 
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
                        type="text" // Assuming clubAge should be a string
                        placeholder="Enter club age"
                        value={clubAge}
                        onChange={(e) => setClubAge(e.target.value)}
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

export default UpdateModals;