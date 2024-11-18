"use client";

import CreateModal from '../../components/modals/clubs/createYourClub.modal';
import { useEffect, useState } from "react";
import { Button, Container, Card, Row, Col, Spinner } from "react-bootstrap";
import { useSelector } from "react-redux";
import { RootState } from "@/lib/store";
import { useRouter } from "next/navigation";
import './style.scss';

const YourClub = () => {
    const [clubs, setClubs] = useState<any[]>([]);
    const [searchResults, setSearchResults] = useState<any[]>([]);
    const [loading, setLoading] = useState(true);
    const [loadingSearch, setLoadingSearch] = useState(false);
    const [errorSearch, setErrorSearch] = useState<string | null>(null); // Lưu lỗi tìm kiếm
    const userId = useSelector((state: RootState) => state.auth.user?.userId);
    const router = useRouter();
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [searchTerm, setSearchTerm] = useState<string>("");

    useEffect(() => {
        const fetchClubs = async () => {
            setLoading(true);
            try {
                const response = await fetch(`${process.env.NEXT_PUBLIC_CLUB}/getall`, {
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
                if (data && data.data) {
                    setClubs(data.data);
                } else {
                    console.error("Unexpected response format:", data);
                }
            } catch (error) {
                console.error("Error fetching clubs data:", error);
            } finally {
                setLoading(false);
            }
        };

        if (userId) {
            fetchClubs();
        }
    }, [userId]);

    const searchClubs = async (term: string) => {
        setLoadingSearch(true);
        setErrorSearch(null);
        try {
            const response = await fetch(`${process.env.NEXT_PUBLIC_CLUB}/findbyName/${encodeURIComponent(term)}`, {
                method: 'GET',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
            });

            if (!response.ok) {
                throw new Error(`Failed to search clubs. Status: ${response.status}`);
            }

            const data = await response.json();
            if (data && data.data && data.data.length > 0) {
                setSearchResults(data.data);
            } else {
                setSearchResults([]);
                setErrorSearch("No clubs found for your search.");
            }
        } catch (error) {
            console.error("Error searching clubs:", error);
            setErrorSearch("An error occurred while searching. Please try again.");
        } finally {
            setLoadingSearch(false);
        }
    };

    // Debounce logic for search input
    useEffect(() => {
        const delayDebounceFn = setTimeout(() => {
            if (searchTerm) {
                searchClubs(searchTerm);
            } else {
                setSearchResults([]);
                setErrorSearch(null);
            }
        }, 300);

        return () => clearTimeout(delayDebounceFn);
    }, [searchTerm]);

    const handleCreateClub = () => {
        setShowModalCreate(true);
    };

    if (loading) {
        return (
            <div className="loading">
                <Spinner animation="border" />
                <p>Loading...</p>
            </div>
        );
    }

    const displayClubs = searchResults.length > 0 ? searchResults : clubs;

    return (
        <Container className="text-center mt-5">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2>List Clubs</h2>
                <div className="d-flex align-items-center">
                    <input
                        type="text"
                        placeholder="Search clubs..."
                        className="form-control me-2"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                    <Button variant="primary" onClick={handleCreateClub}>Create Your Club</Button>
                </div>
            </div>

            {loadingSearch ? (
                <div className="loading">
                    <Spinner animation="border" />
                    <p>Searching...</p>
                </div>
            ) : (
                <>
                    {errorSearch ? (
                        <div className="no-club-banner">
                            <h3>{errorSearch}</h3>
                        </div>
                    ) : (
                        <>
                            {displayClubs.length > 0 ? (
                                <Row xs={1} md={2} lg={3} className="g-4">
                                    {displayClubs.map((club) => (
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
                                    <h3>You don't have any clubs yet!</h3>
                                    <p>Create a club to start managing your team.</p>
                                    <Button variant="primary" onClick={handleCreateClub}>Create Your Club</Button>
                                </div>
                            )}
                        </>
                    )}
                </>
            )}
            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
                userId={userId ? Number(userId) : -1}
            />
        </Container>
    );
};

export default YourClub;
