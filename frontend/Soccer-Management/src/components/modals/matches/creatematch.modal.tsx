import React, { useEffect, useState } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface Club {
    clubID: number;
    clubName: string;
}

interface Matches {
    matchesName: string;
    matchesDescription: string;
    tournamentId: number;
    stadium: string;
    startTime: string;
    endTime: string;
    teamA: number;
    teamB: number;
}

interface CreateMatchAProps {
    showModalCreate: boolean;
    setShowModalCreate: (show: boolean) => void;
}

const CreateMatchA: React.FC<CreateMatchAProps> = ({ showModalCreate, setShowModalCreate }) => {
    const [clubs, setClubs] = useState<Club[]>([]);
    const [teamA, setTeamA] = useState<number | null>(null); // Default to null for teamA
    const [teamB, setTeamB] = useState<number | null>(null); // Default to null for teamB
    const [loading, setLoading] = useState<boolean>(false);
    const [teamASearchTerm, setTeamASearchTerm] = useState<string>(''); // Search term for teamA
    const [teamBSearchTerm, setTeamBSearchTerm] = useState<string>(''); // Search term for teamB
    const [error, setError] = useState<string | null>(null);
    const [matchesName, setMatchesName] = useState<string>('');
    const [matchesDescription, setMatchesDescription] = useState<string>('');
    const [stadium, setStadium] = useState<string>('');
    const [startTime, setStartTime] = useState<string>('');
    const [endTime, setEndTime] = useState<string>('');

    // Fetch clubs from API based on search term
    const fetchClubs = async (searchTerm: string) => {
        if (!searchTerm.trim()) {
            setClubs([]);
            return;
        }

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(
                `${process.env.NEXT_PUBLIC_CLUB}/findbyName/${encodeURIComponent(searchTerm)}`
            );

            if (!response.ok) {
                throw new Error(`Error ${response.status}: ${response.statusText}`);
            }

            const data = await response.json();
            setClubs(data.data || []);
        } catch (error: any) {
            setError(error.message || 'Failed to fetch clubs.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (showModalCreate) {
            const delayDebounce = setTimeout(() => {
                // Fetch clubs based on either teamA or teamB search term
                if (teamASearchTerm) fetchClubs(teamASearchTerm);
                if (teamBSearchTerm) fetchClubs(teamBSearchTerm);
            }, 500);

            return () => clearTimeout(delayDebounce);
        }
    }, [teamASearchTerm, teamBSearchTerm, showModalCreate]);

    // Auto-fill form fields after club selection
    useEffect(() => {
        if (teamA && teamB) {
            const mockMatchData = {
                matchesName: `Match between ${teamA} and ${teamB}`,
                matchesDescription: `Description for match between ${teamA} and ${teamB}`,
                stadium: `Stadium for match`,
                startTime: new Date().toISOString().slice(0, 16), // Current date-time (ISO)
                endTime: new Date(Date.now() + 2 * 60 * 60 * 1000).toISOString().slice(0, 16), // +2 hours
            };

            setMatchesName(mockMatchData.matchesName);
            setMatchesDescription(mockMatchData.matchesDescription);
            setStadium(mockMatchData.stadium);
            setStartTime(mockMatchData.startTime);
            setEndTime(mockMatchData.endTime);
        }
    }, [teamA, teamB]);

    const handleSubmit = () => {
        if (!matchesName || !matchesDescription || !stadium || !startTime || !endTime || !teamA || !teamB) {
            toast.error('All fields are required.');
            return;
        }

        const matchData: Matches = {
            matchesName,
            matchesDescription,
            tournamentId: teamA, // Assuming teamA is the tournament's ID
            stadium,
            startTime: new Date(startTime).toISOString(),
            endTime: new Date(endTime).toISOString(),
            teamA,
            teamB,
        };

        fetch(`${process.env.NEXT_PUBLIC_MATCHES}/createMatches`, {
            headers: {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            },
            method: 'POST',
            body: JSON.stringify(matchData),
        })
            .then((res) => {
                if (!res.ok) throw new Error(`HTTP error ${res.status}`);
                return res.json();
            })
            .then(() => {
                toast.success('Match created successfully.');
                handleCloseModal();
                mutate(`${process.env.NEXT_PUBLIC_MATCHES}/getAllMatches`);
            })
            .catch((err) => {
                toast.error(`Error creating match: ${err.message}`);
            });
    };

    const handleCloseModal = () => {
        setShowModalCreate(false);
        setTeamASearchTerm('');
        setTeamBSearchTerm('');
        setClubs([]);
        setError(null);
        setMatchesName('');
        setMatchesDescription('');
        setStadium('');
        setStartTime('');
        setEndTime('');
        setTeamA(null);
        setTeamB(null);
    };

    return (
        <Modal show={showModalCreate} onHide={handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Create New Match</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Search Team A</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter Team A name"
                        value={teamASearchTerm}
                        onChange={(e) => setTeamASearchTerm(e.target.value)}
                    />
                </Form.Group>
                {loading ? (
                    <div className="text-center">
                        <Spinner animation="border" />
                    </div>
                ) : error ? (
                    <p className="text-center text-danger">{error}</p>
                ) : clubs.length > 0 ? (
                    <Form.Group>
                        <Form.Label>Select Team A</Form.Label>
                        <Form.Control
                            as="select"
                            value={teamA || ''}
                            onChange={(e) => setTeamA(Number(e.target.value))}
                        >
                            <option value="">-- Select Team A --</option>
                            {clubs.map((club) => (
                                <option key={club.clubID} value={club.clubID}>
                                    {club.clubName}
                                </option>
                            ))}
                        </Form.Control>
                    </Form.Group>
                ) : (
                    <p className="text-center text-muted">No clubs found for Team A.</p>
                )}

                <Form.Group className="mb-3 mt-3">
                    <Form.Label>Search Team B</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter Team B name"
                        value={teamBSearchTerm}
                        onChange={(e) => setTeamBSearchTerm(e.target.value)}
                    />
                </Form.Group>
                {loading ? (
                    <div className="text-center">
                        <Spinner animation="border" />
                    </div>
                ) : error ? (
                    <p className="text-center text-danger">{error}</p>
                ) : clubs.length > 0 ? (
                    <Form.Group>
                        <Form.Label>Select Team B</Form.Label>
                        <Form.Control
                            as="select"
                            value={teamB || ''}
                            onChange={(e) => setTeamB(Number(e.target.value))}
                        >
                            <option value="">-- Select Team B --</option>
                            {clubs.map((club) => (
                                <option key={club.clubID} value={club.clubID}>
                                    {club.clubName}
                                </option>
                            ))}
                        </Form.Control>
                    </Form.Group>
                ) : (
                    <p className="text-center text-muted">No clubs found for Team B.</p>
                )}

                <Form.Group className="mt-3">
                    <Form.Label>Match Name</Form.Label>
                    <Form.Control
                        type="text"
                        value={matchesName}
                        onChange={(e) => setMatchesName(e.target.value)}
                    />
                </Form.Group>
                <Form.Group className="mt-3">
                    <Form.Label>Description</Form.Label>
                    <Form.Control
                        type="text"
                        value={matchesDescription}
                        onChange={(e) => setMatchesDescription(e.target.value)}
                    />
                </Form.Group>
                <Form.Group className="mt-3">
                    <Form.Label>Stadium</Form.Label>
                    <Form.Control
                        type="text"
                        value={stadium}
                        onChange={(e) => setStadium(e.target.value)}
                    />
                </Form.Group>
                <Form.Group className="mt-3">
                    <Form.Label>Start Time</Form.Label>
                    <Form.Control
                        type="datetime-local"
                        value={startTime}
                        onChange={(e) => setStartTime(e.target.value)}
                    />
                </Form.Group>
                <Form.Group className="mt-3">
                    <Form.Label>End Time</Form.Label>
                    <Form.Control
                        type="datetime-local"
                        value={endTime}
                        onChange={(e) => setEndTime(e.target.value)}
                    />
                </Form.Group>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Cancel
                </Button>
                <Button variant="primary" onClick={handleSubmit}>
                    Create Match
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default CreateMatchA;
