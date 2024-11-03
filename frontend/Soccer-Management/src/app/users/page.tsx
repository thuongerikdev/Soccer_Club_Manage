'use client';
import Link from "next/link";
import UserTable from "@/components/table/app.usertable";
import useSWR from 'swr';
import { Button, Container, Row, Col, Card, Spinner, Form } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css'; // Ensure Bootstrap CSS is imported
import withAuth from '../../components/middleware/withAuth';
import router from "next/router";

const UserPage = () => {
    const fetcher = (url: string) => fetch(url).then((res) => res.json());

    const { data, error } = useSWR(
        process.env.NEXT_PUBLIC_USER_GETALL,
        fetcher,
        {
            revalidateIfStale: false,
            revalidateOnFocus: false,
            revalidateOnReconnect: false
        }
    );

    if (error) {
        return <div className="alert alert-danger">Error loading user data.</div>;
    }

    if (!data) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
                <Spinner animation="border" variant="primary" />
                <span className="ml-3">Loading...</span>
            </div>
        );
    }

    const handleAddUser = () => {
        // Logic for adding a user
        router.push('/add-user'); // Redirect to the add user page
    };

    return (
        <Container className="mt-4">
            <Row className="mb-4">
                <Col>
                    <h1 className="text-center">User Management</h1>
                    <p className="text-center">Manage your users efficiently. View, edit, or add new users.</p>
                </Col>
            </Row>
            <Row className="mb-4">
                <Col className="d-flex justify-content-between">
                    <Form className="d-flex" style={{ width: '300px' }}>
                        <Form.Control
                            type="search"
                            placeholder="Search users..."
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
                            <h5>Users Overview</h5>
                        </Card.Header>
                        <Card.Body>
                            <p>Total Users: {data.length}</p>
                            <UserTable users={data.sort((a: any, b: any) => b.userId - a.userId)} />
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    );
}

export default withAuth(UserPage);