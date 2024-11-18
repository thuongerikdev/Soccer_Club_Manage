import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';



interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    match: Matches | null;
    setMatch: (value: Matches | null) => void;
}

function UpdateMatchModal(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, match, setMatch } = props;

    const [matchesId, setMatchesId] = useState<number>(0);
    const [matchesName, setMatchesName] = useState<string>('');
    const [matchesDescription, setMatchesDescription] = useState<string>('');
    const [teamA, setTeamA] = useState<number>(0);
    const [teamB, setTeamB] = useState<number>(0);
    const [tournamentID, setTournamentID] = useState<number | ''>(0);
    const [stadium, setStadium] = useState<string>('');
    const [startTime, setStartTime] = useState<string>('');
    const [endTime, setEndTime] = useState<string>('');

    useEffect(() => {
        if (match && match.matchesID) {
            setMatchesId(match.matchesID);
            setMatchesName(match.matchesName || '');
            setMatchesDescription(match.matchesDescription || '');
            setTeamA(match.teamA || 0);
            setTeamB(match.teamB || 0);
            setTournamentID(match.tournamentID || 0);
            setStadium(match.stadium || '');
            setStartTime(match.startTime || '');
            setEndTime(match.endTime || '');
        }
    }, [match]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_MATCHES}/updateMatches`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify({
                matchesID: matchesId,               // Match ID should be in the payload
                matchesName,                        // Match name field
                teamA,                              // Team A
                teamB,                              // Team B
                matchesDescription,                 // Description of the match
                tournamentID,                       // Tournament ID
                stadium,                            // Stadium name
                startTime,                          // Match start time
                endTime,                            // Match end time
            }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success("Update successful");
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_MATCHES}/getAllMatches`);
            }
        })
        .catch(err => {
            toast.error("Error updating match");
            console.error(err);
        });
    };

    const handleCloseModal = () => {
        setMatchesName("");
        setMatchesDescription("");
        setTeamA(0);
        setTeamB(0);
        setTournamentID(0);
        setStadium("");
        setStartTime("");
        setEndTime("");
        setShowModalUpdate(false);
        setMatch(null);
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
                <Modal.Title>Update Match</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Match Name</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter match name" 
                        value={matchesName}
                        onChange={(e) => setMatchesName(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Match Description</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter match description" 
                        value={matchesDescription}
                        onChange={(e) => setMatchesDescription(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Team A</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter Team A ID" 
                        value={teamA === 0 ? '' : teamA}
                        onChange={(e) => setTeamA(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Team B</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter Team B ID" 
                        value={teamB === 0 ? '' : teamB}
                        onChange={(e) => setTeamB(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Tournament ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter tournament ID" 
                        value={tournamentID === 0 ? '' : tournamentID}
                        onChange={(e) => setTournamentID(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Stadium</Form.Label>
                    <Form.Control 
                        type="text" 
                        placeholder="Enter stadium name" 
                        value={stadium}
                        onChange={(e) => setStadium(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Start Time</Form.Label>
                    <Form.Control 
                        type="datetime-local" 
                        placeholder="Enter start time" 
                        value={startTime}
                        onChange={(e) => setStartTime(e.target.value)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>End Time</Form.Label>
                    <Form.Control 
                        type="datetime-local" 
                        placeholder="Enter end time" 
                        value={endTime}
                        onChange={(e) => setEndTime(e.target.value)} 
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

export default UpdateMatchModal;
