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

interface Matches {
    matchesName: string; // Changed to string based on API input
    matchesDescription: string;
    tournamentId: number;
    stadium: string;
    startTime: string; // Ensure these are formatted correctly
    endTime: string;
    teamWin: number; // Optional based on your requirements
    teamLose: number; // Optional based on your requirements
}

function CreateMatchModal({ showModalCreate, setShowModalCreate }: IProps) {
    const [matchesName, setMatchesName] = useState<string>('');
    const [matchesDescription, setMatchesDescription] = useState<string>('');
    const [tournamentId, setTournamentId] = useState<number | ''>(0);
    const [stadium, setStadium] = useState<string>('');
    const [startTime, setStartTime] = useState<string>('');
    const [endTime, setEndTime] = useState<string>('');
    const [teamWin, setTeamWin] = useState<number | ''>(0);
    const [teamLose, setTeamLose] = useState<number | ''>(0);

    const handleSubmit = () => {
        // Input validation
        if (!matchesName || !matchesDescription || tournamentId === 0 || !stadium || !startTime || !endTime) {
            toast.error("All fields are required.");
            return;
        }

        const matchData: Matches = {
            matchesName, // Ensure this is a string
            matchesDescription,
            tournamentId: Number(tournamentId),
            stadium,
            startTime: new Date(startTime).toISOString(), // Ensure proper formatting
            endTime: new Date(endTime).toISOString(),     // Ensure proper formatting
            teamWin: Number(teamWin), // Optional if needed
            teamLose: Number(teamLose), // Optional if needed
        };

        console.log("Submitting match data:", matchData); // Log the data being sent

        fetch(`${process.env.NEXT_PUBLIC_MATCHES}/create`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "POST",
            body: JSON.stringify(matchData),
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
                mutate(`${process.env.NEXT_PUBLIC_MATCHES}/getall`); // Adjust API endpoint as needed
            }
        })
        .catch(err => {
            toast.error("Error creating match: " + err.message); // Log the error message
            console.error("Error details:", err);
        });
    };

    const handleCloseModal = () => {
        setMatchesName('');
        setMatchesDescription('');
        setTournamentId(0);
        setStadium('');
        setStartTime('');
        setEndTime('');
        setTeamWin(0);
        setTeamLose(0);
        setShowModalCreate(false); // Close modal after submission
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
                <Modal.Title>Create Match</Modal.Title>
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

export default CreateMatchModal;