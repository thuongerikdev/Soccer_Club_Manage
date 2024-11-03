'use client';
import Link from "next/link";
import PlayerTable from "@/components/table/app.playertable"; // Ensure this path is correct
import useSWR from 'swr';
import { Button, Container, Row, Col, Card, Spinner, Form } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; // Ensure Bootstrap CSS is imported
import withAuth from '../../components/middleware/withAuth';
const PlayerPage = () => {
    const fetcher = (url: string) => fetch(url).then((res) => res.json());

    const { data, error } = useSWR(
        `http://localhost:3001/api/players/getall`,
        fetcher,
        {
            revalidateIfStale: false,
            revalidateOnFocus: false,
            revalidateOnReconnect: false
        }
    );

    if (error) {
        return <div className="alert alert-danger">Error loading player data.</div>;
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
                    <h1 className="text-center">Player Management</h1>
                    <p className="text-center">Manage your players efficiently. View, edit, or add new players.</p>
                </Col>
            </Row>
            <Row className="mb-4">
                <Col className="d-flex justify-content-between">
                   
                    <Form className="d-flex" style={{ width: '300px' }}>
                        <Form.Control
                            type="search"
                            placeholder="Search players..."
                            className="me-2"
                            aria-label="Search"
                        />
                        <Button variant="outline-success">Search</Button>
                    </Form>
                </Col>
            </Row>
            <Row>
                <Col>
                    <Card>
                        <Card.Header>
                            <h5>Players Overview</h5>
                        </Card.Header>
                        <Card.Body>
                            <p>Total Players: {data.dt.length}</p>
                            <PlayerTable players={data.dt.sort((a: any, b: any) => b.PlayerId - a.PlayerId)} />
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
}

export default withAuth(PlayerPage);