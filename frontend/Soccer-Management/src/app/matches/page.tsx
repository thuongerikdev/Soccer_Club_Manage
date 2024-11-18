'use client';
import Link from "next/link";
import MatchesTable from "@/components/table/app.matchestable"; // Ensure you have this component
import useSWR from 'swr';
import { useState } from 'react';
import { Button, Container, Row, Col, Card, Spinner, Alert, InputGroup, FormControl } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; // Ensure Bootstrap CSS is imported
import withAuth from '../../components/middleware/withAuth';


const MatchesPage = () => {
    const fetcher = (url: string) => fetch(url).then((res) => res.json());

    const { data, error } = useSWR(
        `${process.env.NEXT_PUBLIC_MATCHES}/getAllMatches`, // API to fetch all matches
        fetcher,
        {
            revalidateIfStale: false,
            revalidateOnFocus: false,
            revalidateOnReconnect: false
        }
    );

    const [searchQuery, setSearchQuery] = useState('');

    if (error) {
        return <Alert variant="danger">Error loading match data: {error.message}</Alert>;
    }

    if (!data) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
                <Spinner animation="border" variant="primary" />
                <span className="ml-3">Loading...</span>
            </div>
        );
    }

    const totalMatches = data.data.length;

    // Filter matches based on the search query
    const filteredMatches = data.data.filter((match: Matches) => {
        const name = match.matchesName.toString(); // Ensure matchesName is a string
        return name.toLowerCase().includes(searchQuery.toLowerCase());
    });

    return (
        <Container className="mt-4">
            <Row className="mb-4">
                <Col>
                    <h1 className="text-center">Match Management</h1>
                    <p className="text-center">Efficiently manage your matches. View, edit, or create new matches.</p>
                </Col>
            </Row>
            <Row className="mb-4">
                <Col>
                    <Card>
                        <Card.Header>
                            <h5>Match Overview</h5>
                        </Card.Header>
                        <Card.Body>
                            <p>Total Matches: {totalMatches}</p>
                            <p>
                                Most Recent Match: {Array.isArray(data.data) && data.data.length > 0 ? data.data[0].matchesName : "N/A"}
                            </p>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row className="mb-4">
                <Col>
                   
                </Col>
            </Row>
            <Row>
                <Col>
                    <MatchesTable matches={filteredMatches.sort((a: Matches, b: Matches) => b.matchesID - a.matchesID)} />
                </Col>
            </Row>
            <Row className="mt-4">
                <Col className="text-center">
                    <Link href="/help" className="btn btn-link">Need Help?</Link>
                </Col>
            </Row>
        </Container>
    );
}

export default withAuth(MatchesPage);