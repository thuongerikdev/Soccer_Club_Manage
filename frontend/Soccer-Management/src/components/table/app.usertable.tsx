import { Button, Table, Card, Container, Row, Col, Form } from 'react-bootstrap';
import CreateModal from '../modals/users/createUser.modal';
import DeleteModal from '../modals/users/deleteUser.modal';
import UpdateModals from '../modals/users/updateUser.modal';
import { useState } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';

interface IProps {
    users: IUser[];
    className?: string;
}

const UserTable = ({ users, className }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [user, setUser] = useState<IUser | null>(null);
    const [userId, setUserId] = useState<number>(0);
    const [searchId, setSearchId] = useState<number | ''>('');

    const handleUpdate = (item: IUser) => {
        setUser(item);
        setShowModalUpdate(true);
    };

    const handleDelete = (id: number) => {
        setUserId(id);
        setShowModalDelete(true);
    };

    const handleCreate = () => {
        setShowModalCreate(true);
    };

    // Filter users based on search ID
    const filteredUsers = users.filter(user => 
        searchId === '' || user.userId === searchId
    );

    return (
        <Container className={`mt-4 ${className}`}>
            <Row className='mb-3 d-flex justify-content-between align-items-center'>
                <Col>
                    <h3>User Management</h3>
                    <p>Manage your users efficiently. View, edit, or add new users.</p>
                </Col>
                <Col xs="auto">
                    <Button variant="primary" onClick={handleCreate}>Add New User</Button>
                </Col>
            </Row>

            <Row className="mb-3">
                <Col>
                    <Form.Group>
                        <Form.Label>Search by ID</Form.Label>
                        <Form.Control 
                            type="number" 
                            placeholder="Enter user ID" 
                            value={searchId} 
                            onChange={(e) => setSearchId(e.target.value ? parseInt(e.target.value) : '')}
                        />
                    </Form.Group>
                </Col>
            </Row>

            <Row>
                <Col>
                    <Card>
                        <Card.Header>
                            <h5>User Overview</h5>
                        </Card.Header>
                        <Card.Body>
                            <Table striped bordered hover responsive>
                                <thead className="table-light">
                                    <tr>
                                        <th>ID</th>
                                        <th>Username</th>
                                        <th>Name</th>
                                        <th>Email</th>
                                        <th>Age</th>
                                        <th>Address</th>
                                        <th>Gender</th>
                                        <th>Phone</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredUsers.length > 0 ? filteredUsers.map(item => (
                                        <tr key={item.userId}>
                                            <td>{item.userId}</td>
                                            <td>{item.username}</td>
                                            <td>{item.name}</td>
                                            <td>{item.email}</td>
                                            <td>{item.age}</td>
                                            <td>{item.address}</td>
                                            <td>{item.gender}</td>
                                            <td>{item.phone}</td>
                                            <td>
                                                <div className="d-flex justify-content-around">
                                                    <Button variant='warning' className='mx-2 btn-sm' onClick={() => handleUpdate(item)}>
                                                        <FaEdit /> Update  
                                                    </Button>
                                                    <Button variant='danger' className='btn-sm' onClick={() => handleDelete(item.userId)}>
                                                        <FaTrash /> Delete
                                                    </Button>
                                                </div>
                                            </td>
                                        </tr>
                                    )) : (
                                        <tr>
                                            <td colSpan={9} className="text-center">No users found</td>
                                        </tr>
                                    )}
                                </tbody>
                            </Table>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>

            {/* Modals for Create, Update, Delete */}
            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
            />
            <UpdateModals
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                user={user}
                setUser={setUser}
            />
            <DeleteModal
                showModalDelete={showModalDelete}
                setShowModalDelete={setShowModalDelete}
                userId={userId}
                setUserId={setUserId}
            />
        </Container>
    );
};

export default UserTable;