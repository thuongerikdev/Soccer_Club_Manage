import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    match: IMatch | null;
    setMatch: (value: IMatch | null) => void;
}

interface IMatch {
    matchesId: number;
    matchesName: string;
    matchesDescription: string;
    tournamentId: number;
    stadium: string;
    startTime: string;
    endTime: string;
    teamWin: number;
    teamLose: number;
}

function UpdateMatchModal(props: IProps) {
    const { showModalUpdate, setShowModalUpdate, match, setMatch } = props;

    const [matchesId, setMatchesId] = useState<number>(0);
    const [matchesName, setMatchesName] = useState<string>('');
    const [matchesDescription, setMatchesDescription] = useState<string>('');
    const [tournamentId, setTournamentId] = useState<number | ''>(0);
    const [stadium, setStadium] = useState<string>('');
    const [startTime, setStartTime] = useState<string>('');
    const [endTime, setEndTime] = useState<string>('');
    const [teamWin, setTeamWin] = useState<number | ''>(0);
    const [teamLose, setTeamLose] = useState<number | ''>(0);

    useEffect(() => {
        if (match && match.matchesId) {
            setMatchesId(match.matchesId);
            setMatchesName(match.matchesName || '');
            setMatchesDescription(match.matchesDescription || '');
            setTournamentId(match.tournamentId || 0);
            setStadium(match.stadium || '');
            setStartTime(match.startTime || '');
            setEndTime(match.endTime || '');
            setTeamWin(match.teamWin || 0);
            setTeamLose(match.teamLose || 0);
        }
    }, [match]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_MATCHES}/update/${matchesId}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify({
                matchesId,               // Include match ID
                matchesName,             // Map to expected API input
                matchesDescription,
                tournamentId,
                stadium,
                startTime,
                endTime,
                teamWin,
                teamLose,
            }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success("Update successful"); // Adjusted to match the expected response key
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_MATCHES}/getall`); // Adjust endpoint as necessary
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
        setTournamentId(0);
        setStadium("");
        setStartTime("");
        setEndTime("");
        setTeamWin(0);
        setTeamLose(0);
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
                    <Form.Label>Tournament ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter tournament ID" 
                        value={tournamentId === 0 ? '' : tournamentId}
                        onChange={(e) => setTournamentId(e.target.value ? Number(e.target.value) : 0)} 
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

                <Form.Group className="mb-3">
                    <Form.Label>Team Win</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter winning team ID" 
                        value={teamWin === 0 ? '' : teamWin}
                        onChange={(e) => setTeamWin(e.target.value ? Number(e.target.value) : 0)} 
                    />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Team Lose</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter losing team ID" 
                        value={teamLose === 0 ? '' : teamLose}
                        onChange={(e) => setTeamLose(e.target.value ? Number(e.target.value) : 0)} 
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