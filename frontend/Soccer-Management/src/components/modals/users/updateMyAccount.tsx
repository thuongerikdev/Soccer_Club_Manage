import { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IUser {
    userId: number;
    username: string;
    email: string;
    age: number;
    address: string;
    gender: string;
    phone: number;
    name: string;
}

interface IProps {
    showModalUpdate: boolean;
    setShowModalUpdate: (value: boolean) => void;
    userID: number | undefined; // UserID passed from AppHeader
}

function UpdateModals({ showModalUpdate, setShowModalUpdate, userID }: IProps) {
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [age, setAge] = useState<number | ''>(0);
    const [address, setAddress] = useState<string>('');
    const [gender, setGender] = useState<string>('');
    const [phone, setPhone] = useState<number | ''>(0);
    const [name, setName] = useState<string>('');

    useEffect(() => {
        if (userID) {
            // Fetch user data based on userID
            fetch(`${process.env.NEXT_PUBLIC_USER}/getbyID/${userID}`)
                .then(res => res.json())
                .then(data => {
                    console.log(data); // Log the response to check its structure
                    if (data && data.dt) {
                        // Assuming the API returns an IUser object wrapped in a data object
                        setUsername(data.dt.username);
                        setEmail(data.dt.email);
                        setAge(data.dt.age);
                        setAddress(data.dt.address);
                        setGender(data.dt.gender);
                        setPhone(data.dt.phone);
                        setName(data.dt.name);
                    } else {
                        toast.error('User not found');
                    }
                })
                .catch(err => {
                    console.error(err);
                    toast.error('Failed to fetch user data');
                });
        }
    }, [userID, showModalUpdate]); // Re-fetch when userID or modal visibility changes

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_USER}/update/${userID}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "PUT",
            body: JSON.stringify({ username, password, email, age, address, gender, phone, name }),
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success(res.em);
                handleCloseModal();
                mutate(process.env.NEXT_PUBLIC_USER_GETALL);
            }
        });
    };

    const handleCloseModal = () => {
        setUsername("");
        setPassword("");
        setEmail("");
        setAge(0);
        setAddress("");
        setGender("");
        setPhone(0);
        setName("");
        setShowModalUpdate(false);
    };

    return (
        <Modal
            show={showModalUpdate}
            onHide={handleCloseModal}
            backdrop="static"
            keyboard={false}
            size='lg'
        >
            <Modal.Header closeButton>
                <Modal.Title>Update User</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Username</Form.Label>
                    <Form.Control type="text" placeholder="Enter username" value={username}
                        onChange={(e) => setUsername(e.target.value)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Enter password" value={password}
                        onChange={(e) => setPassword(e.target.value)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" value={email}
                        onChange={(e) => setEmail(e.target.value)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Age</Form.Label>
                    <Form.Control type="number" value={age === 0 ? '' : age}
                        onChange={(e) => setAge(e.target.value ? Number(e.target.value) : 0)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Address</Form.Label>
                    <Form.Control type="text" value={address}
                        onChange={(e) => setAddress(e.target.value)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Phone</Form.Label>
                    <Form.Control type="number" value={phone === 0 ? '' : phone}
                        onChange={(e) => setPhone(e.target.value ? Number(e.target.value) : 0)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Gender</Form.Label>
                    <Form.Control type="text" value={gender}
                        onChange={(e) => setGender(e.target.value)} />
                </Form.Group>

                <Form.Group className="mb-3">
                    <Form.Label>Name</Form.Label>
                    <Form.Control type="text" value={name}
                        onChange={(e) => setName(e.target.value)} />
                </Form.Group>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>Save</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default UpdateModals;