import { useState } from 'react';
import { Form } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalCreate: boolean;
    setShowModalCreate: (value: boolean) => void;
}

function CreateModal(props: IProps) {
    const { showModalCreate, setShowModalCreate } = props;
    const [username, setUsername] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [name , setName] = useState<string>('')
    const [email, setEmail] = useState<string>('');
    
    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_USER}/register` , {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            body: JSON.stringify({ username, password, email  ,name})
        })
        .then(res => res.json())
        .then(res => {
            if (res) {
                toast.success("Create successful");
                handleCloseModal();
                mutate(process.env.NEXT_PUBLIC_USER_GETALL);
            }
        });
    };

    const handleCloseModal = () => {
        setUsername('');
        setPassword('');
        setEmail('');
        setName('');
        setShowModalCreate(false);
    };

    return (
        <Modal
            show={showModalCreate}
            onHide={() => handleCloseModal()}
            backdrop="static"
            keyboard={false}
            size='lg'
        >
            <Modal.Header closeButton>
                <Modal.Title>Create User</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form.Group className="mb-3">
                    <Form.Label>Username</Form.Label>
                    <Form.Control type="text" placeholder="Enter username" value={username}
                        onChange={(e) => setUsername(e.target.value)} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="text" placeholder="Enter password" value={password}
                        onChange={(e) => setPassword(e.target.value)} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Email</Form.Label>
                    <Form.Control type="email" placeholder="Enter email" value={email}
                        onChange={(e) => setEmail(e.target.value)} />
                </Form.Group>
                <Form.Group className="mb-3">
                    <Form.Label>Name</Form.Label>
                    <Form.Control type="text" placeholder="Enter name" value={name}
                        onChange={(e) => setName(e.target.value)} />
                </Form.Group>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => handleCloseModal()}>
                    Close
                </Button>
                <Button variant="primary" onClick={() => handleSubmit()}>Save</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default CreateModal;
