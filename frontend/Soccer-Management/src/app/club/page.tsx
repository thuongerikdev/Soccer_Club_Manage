'use client';
import Link from "next/link";
import ClubTable from "@/components/table/app.clubtable";
import useSWR from 'swr';
import { useState ,useEffect } from 'react';
import { Button, Container, Row, Col, Card, Spinner, Alert, InputGroup, FormControl } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; // Ensure Bootstrap CSS is imported
import withAuth from '../../components/middleware/withAuth';

interface Club {
    clubId: number;
    clubName: string;
    budget: number;
}

const ClubPage = () => {
    const fetcher = (url: string) => fetch(url).then((res) => res.json());

  
    const { data, error } = useSWR(
       `${process.env.NEXT_PUBLIC_CLUB}/getall`, // API
        fetcher,
        {
            revalidateIfStale: false,
            revalidateOnFocus: false,
            revalidateOnReconnect: false
        }
    );

    const [searchQuery, setSearchQuery] = useState('');

    if (error) {
        return <Alert variant="danger">Error loading club data: {error.message}</Alert>;
    }

    if (!data) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
                <Spinner animation="border" variant="primary" />
                <span className="ml-3">Loading...</span>
            </div>
        );
    }

    const totalClubs = data.data.length;
    const averageBudget = totalClubs > 0 
        ? (data.data.reduce((acc: number, club: Club) => acc + club.budget, 0) / totalClubs).toFixed(2) 
        : 0;

    // Lọc câu lạc bộ dựa trên truy vấn tìm kiếm
    const filteredClubs = data.data.filter((club: Club) => 
        club.clubName.toLowerCase().includes(searchQuery.toLowerCase())
    );

    return (
        <Container className="mt-4">
            <Row className="mb-4">
                <Col>
                    <h1 className="text-center">Club Management</h1>
                    <p className="text-center">Efficiently manage your clubs. View, edit, or create new clubs.</p>
                </Col>
            </Row>
            <Row className="mb-4">
                <Col className="d-flex justify-content-center">
                   
                </Col>
            </Row>
            <Row className="mb-4">
                <Col>
                    <Card>
                        <Card.Header>
                            <h5>Club Overview</h5>
                        </Card.Header>
                        <Card.Body>
                            <p>Total Clubs: {totalClubs}</p>
                            <p>Average Budget: ${averageBudget}</p>
                            <p>Most Recent Club: {data.data[0]?.clubName || "N/A"}</p>
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
                    <ClubTable clubs={filteredClubs.sort((a: Club, b: Club) => b.clubId - a.clubId)} />
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

export default withAuth(ClubPage);