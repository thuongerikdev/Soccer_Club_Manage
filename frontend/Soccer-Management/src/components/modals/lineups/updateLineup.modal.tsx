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

    const [lineUpId, setLineUpId] = useState<number>(0);
    const [lineUpName, setLineUpName] = useState<string>('');
    const [lineUpType, setLineUpType] = useState<string>('');
    const [matchType, setMatchType] = useState<string>('');
    const [stadiumBackground, setStadiumBackground] = useState<string>('');
    const [clubId, setClubId] = useState<number | ''>(0);
    const [matchId, setMatchId] = useState<number | ''>(0);

    useEffect(() => {
        if (lineUp) {
            setLineUpId(lineUp.lineUpId);
            setLineUpName(lineUp.lineUpName || '');
            setLineUpType(lineUp.lineUpType || '');
            setMatchType(lineUp.matchType || '');
            setStadiumBackground(lineUp.stadiumBackGround || '');
            setClubId(lineUp.clubId || 0);
            setMatchId(lineUp.matchId || 0);
        }
    }, [lineUp]);

    const handleSubmit = () => {
        // Input validation
        if (!lineUpName || !lineUpType || !matchType || !stadiumBackground || clubId === 0 || matchId === 0) {
            toast.error("All fields are required.");
            return;
        }

        const lineUpData = {
            lineUpId, // Include lineUpId
            matchId: Number(matchId),
            clubId: Number(clubId),
            lineUpName,
            lineUpType,
            matchType,
            stadiumBackGroud: stadiumBackground, // Correct spelling for API
        };

        fetch(`${process.env.NEXT_PUBLIC_LINEUP}/update`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify(lineUpData),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success(res.EM); // Adjusted to match the expected response key
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_LINEUP}/getall`);
            }
        })
        .catch(err => {
            toast.error("Error updating lineup");
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        setLineUpName("");
        setLineUpType("");
        setMatchType("");
        setStadiumBackground("");
        setClubId(0);
        setMatchId(0);
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
                    <Form.Label>Match Type</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter match type" 
                        value={matchType}
                        onChange={(e) => setMatchType(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Stadium Background URL</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter stadium background URL" 
                        value={stadiumBackground}
                        onChange={(e) => setStadiumBackground(e.target.value)} 
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
                    <Form.Label>Match ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter match ID" 
                        value={matchId === 0 ? '' : matchId}
                        onChange={(e) => setMatchId(e.target.value ? Number(e.target.value) : 0)} 
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