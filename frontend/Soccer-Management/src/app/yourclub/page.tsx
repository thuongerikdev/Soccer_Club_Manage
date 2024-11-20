"use client";

import CreateModal from '../../components/modals/clubs/createYourClub.modal';
import { useEffect, useState } from "react";
import { Button, Container, Card, Row, Col } from "react-bootstrap";
import { useSelector } from "react-redux";
import { RootState } from "@/lib/store"; // Adjust the import path as needed
import { useRouter } from "next/navigation";
import useSWR from 'swr'; // Import SWR
import './style.scss'; // Import the style file

const fetcher = (url: string) => fetch(url, {
    method: 'GET',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    },
}).then(res => {
    if (!res.ok) {
        throw new Error(`Failed to fetch: ${res.status}`);
    }
    return res.json();
});

const YourClub = () => {
    const userId = useSelector((state: RootState) => state.auth.user?.userId); // Get userId from Redux state
    const router = useRouter();
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);

    // Use SWR to fetch clubs data
    const { data, error } = useSWR(userId ? `${process.env.NEXT_PUBLIC_CLUB}/getyourClub/${userId}` : null, fetcher);

    const handleCreateClub = () => {
        setShowModalCreate(true);
    };

    if (!data && !error) {
        return <div className="loading">Loading...</div>;
    }

    if (error) {
        console.error("Error fetching clubs data:", error);
        return <div>Error fetching clubs data.</div>;
    }

    const clubs = data?.data || []; // Use the fetched data

    return (
        <Container className="text-center mt-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>Your Clubs</h2>
                <Button variant="primary" onClick={handleCreateClub}>Create Your Club</Button>
            </div>

            {clubs.length > 0 ? (
                <Row xs={1} md={2} lg={3} className="g-4">
                    {clubs.map((club: IClub) => (
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