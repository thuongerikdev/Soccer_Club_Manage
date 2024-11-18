"use client";

import CreateModal from '../../components/modals/clubs/createYourClub.modal';
import { useEffect, useState } from "react";
import { Button, Container, Card, Row, Col } from "react-bootstrap";
import { useSelector } from "react-redux";
import { RootState } from "@/lib/store"; // Adjust the import path as needed
import { useRouter } from "next/navigation";
import './style.scss'; // Import the style file

const YourClub = () => {
    const [clubs, setClubs] = useState<any[]>([]); // Store club data
    const [loading, setLoading] = useState(true); // Loading state
    const userId = useSelector((state: RootState) => state.auth.user?.userId); // Get userId from Redux state
    const router = useRouter();
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);

    useEffect(() => {
        const fetchClubs = async () => {
            setLoading(true); // Start loading state
            try {
                const response = await fetch(`${process.env.NEXT_PUBLIC_CLUB}/getyourClub/${userId}`, {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json',
                    },
                });

                if (!response.ok) {
                    throw new Error(`Failed to fetch clubs data. Status: ${response.status}`);
                }

                const data = await response.json();
                setClubs(data.data || []);
            } catch (error) {
                console.error("Error fetching clubs data:", error);
            } finally {
                setLoading(false);
            }
        };

        if (userId) {
            fetchClubs(); // Fetch clubs data if userId is available
        }
    }, [userId]);

    const handleCreateClub = () => {
        setShowModalCreate(true);
    };

    if (loading) {
        return <div className="loading">Loading...</div>;
    }

    return (
        <Container className="text-center mt-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>Your Clubs</h2>
                <Button variant="primary" onClick={handleCreateClub}>Create Your Club</Button>
            </div>

            {clubs.length > 0 ? (
                <Row xs={1} md={2} lg={3} className="g-4">
                    {clubs.map((club) => (
                        <Col key={club.clubID}>
                            <Card className="club-card">
                                <Card.Img variant="top" src={'/slider1.png'} alt={`${club.clubName} logo`} />
                                <Card.Body>
                                    <Card.Title>{club.clubName}</Card.Title>
                                    <Card.Text>{club.clubDescription}</Card.Text>
                                    <Button variant="primary" onClick={() => router.push(`/yourclub/${club.clubID}`)}>View Club</Button>
                                </Card.Body>
                            </Card>
                        </Col>
                    ))}
                </Row>
            ) : (
                <div className="no-club-banner">
                    <h3>You don't have a club yet!</h3>
                    <p>Create a club to start managing your team.</p>
                    <Button variant="primary" onClick={handleCreateClub}>Create Your Club</Button>
                </div>
            )}
            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
                userId={Number(userId) || -1}
            />
        </Container>
    );
};

export default YourClub;
