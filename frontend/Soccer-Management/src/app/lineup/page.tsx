'use client';
import Link from "next/link";
import LineUpTable from "@/components/table/app.lineuptable"; // Ensure this path is correct
import useSWR from 'swr';
import { Button, Container, Row, Col, Card, Spinner, Form } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; // Ensure Bootstrap CSS is imported
import withAuth from '../../components/middleware/withAuth';

const LineUpPage = () => {
    const fetcher = (url: string) => fetch(url).then((res) => res.json());

    const { data, error } = useSWR(
        `http://localhost:3001/api/lineup/getall`, // Adjust to your API endpoint
        fetcher,
        {
            revalidateIfStale: false,
            revalidateOnFocus: false,
            revalidateOnReconnect: false
        }
    );

    if (error) {
        return <div className="alert alert-danger">Error loading lineup data.</div>;
    }

    if (!data) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
                <Spinner animation="border" variant="primary" />
                <span className="ml-3">Loading...</span>
            </div>
        );
    }

    return (
        <Container className="mt-4">
            <Row className="mb-4">
                <Col>
                    <h1 className="text-center">Lineup Management</h1>
                    <p className="text-center">Manage your lineups efficiently. View, edit, or add new lineups.</p>
                </Col>
            </Row>
            <Row className="mb-4">
                <Col className="d-flex justify-content-between">
                   
                </Col>
            </Row>
            <Row>
                <Col>
                    <Card>
                        <Card.Header>
                            <h5>Lineups Overview</h5>
                        </Card.Header>
                        <Card.Body>
                            <p>Total Lineups: {data.data.length}</p>
                            <LineUpTable lineUps={data.data.sort((a: any, b: any) => b.lineUpId - a.lineUpId)} />
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
}

export default withAuth(LineUpPage);