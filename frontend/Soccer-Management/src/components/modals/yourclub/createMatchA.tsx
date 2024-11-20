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
    clubID: number; // clubID passed from parent component
}

const CreateMatchA: React.FC<CreateMatchAProps> = ({ showModalCreate, setShowModalCreate, clubID }) => {
    const [clubs, setClubs] = useState<Club[]>([]);
    const [selectedClub, setSelectedClub] = useState<number | null>(null); // Default to null, allowing user to select
    const [loading, setLoading] = useState<boolean>(false);
    const [searchTerm, setSearchTerm] = useState<string>('');
    const [error, setError] = useState<string | null>(null);
    const [matchesName, setMatchesName] = useState<string>('');
    const [matchesDescription, setMatchesDescription] = useState<string>('');
    const [stadium, setStadium] = useState<string>('');
    const [startTime, setStartTime] = useState<string>('');
    const [endTime, setEndTime] = useState<string>('');

    // Fetch clubs from API based on search term
    const fetchClubs = async () => {
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
            mutate(`${process.env.NEXT_PUBLIC_MATCHES}/TeamA/${clubID}`);
        } catch (error: any) {
            setError(error.message || 'Failed to fetch clubs.');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (showModalCreate) {
            const delayDebounce = setTimeout(() => {
                fetchClubs();
            }, 500);

            return () => clearTimeout(delayDebounce);
        }
    }, [searchTerm, showModalCreate]);

    // Auto-fill form fields after club selection
    useEffect(() => {
        if (selectedClub) {
            const mockMatchData = {
                matchesName: `Match between ${clubID} and ${selectedClub}`,
                matchesDescription: `Description for match between ${clubID} and ${selectedClub}`,
                stadium: `Stadium for match`,
                startTime: new Date().toISOString().slice(0, 16), // Current date-time (ISO)
                endTime: new Date(Date.now() + 2 * 60 * 60 * 1000).toISOString().slice(0, 16), // +2 hours
                teamA: clubID, // teamA is the clubID from parent component
                teamB: selectedClub, // teamB is the selectedClub (from modal)
            };

            setMatchesName(mockMatchData.matchesName);
            setMatchesDescription(mockMatchData.matchesDescription);
            setStadium(mockMatchData.stadium);
            setStartTime(mockMatchData.startTime);
            setEndTime(mockMatchData.endTime);
        }
    }, [selectedClub, clubID]);

    const handleSubmit = () => {
        if (!matchesName || !matchesDescription || !stadium || !startTime || !endTime || !selectedClub) {
            toast.error('All fields are required.');
            return;
        }

        const matchData: Matches = {
            matchesName,
            matchesDescription,
            tournamentId: clubID, // tournamentId is still the clubID from parent
            stadium,
            startTime: new Date(startTime).toISOString(),
            endTime: new Date(endTime).toISOString(),
            teamA: clubID, // teamA is the clubID from parent component
            teamB: selectedClub, // teamB is the selected club (asserted to be a number)
        };
        console.log("check data" , matchData)

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
                mutate(`${process.env.NEXT_PUBLIC_MATCHES}/TeamA/${clubID}`);
                handleCloseModal();
          
            })
            .catch((err) => {
                toast.error(`Error creating match: ${err.message}`);
            });
    };

    const handleCloseModal = () => {
        setShowModalCreate(false);
        setSearchTerm('');
        setClubs([]);
        setError(null);
        setMatchesName('');
        setMatchesDescription('');
        setStadium('');
        setStartTime('');
        setEndTime('');
        setSelectedClub(null);
    };

    return (
        <Modal show={showModalCreate} onHide={handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Create New Match</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Search Club</Form.Label>
                    <Form.Control
                        type="text"
                        placeholder="Enter club name"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
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
                        <Form.Label>Select a Club</Form.Label>
                        <Form.Control
                            as="select"
                            value={selectedClub || ''}
                            onChange={(e) => setSelectedClub(Number(e.target.value))}
                        >
                            <option value="">-- Select a Club --</option>
                            {clubs.map((club) => (
                                <option key={club.clubID} value={club.clubID}>
                                    {club.clubName}
                                </option>
                            ))}
                        </Form.Control>
                    </Form.Group>
                ) : (
                    <p className="text-center text-muted">No clubs found.</p>
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
                <Button variant="primary" onClick={handleSubmit} disabled={!selectedClub}>
                    Save
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default CreateMatchA;
