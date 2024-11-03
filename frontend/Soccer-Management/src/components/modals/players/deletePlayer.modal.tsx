import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalDelete: boolean;
    setShowModalDelete: (value: boolean) => void;
    playerId: number; // Đổi từ userId thành playerId
    setPlayerId: (value: number) => void; // Đổi từ setUserId thành setPlayerId
}

function DeletePlayerModal(props: IProps) {
    const { showModalDelete, setShowModalDelete, playerId, setPlayerId } = props;
    const [id, setId] = useState<number>(0);

    useEffect(() => {
        setId(playerId);
    }, [playerId]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_PLAYER}/remove/${id}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            method: "DELETE",
        })
            .then(res => res.json())
            .then(res => {
                if (res) {
                    toast.success("Delete successful");
                    handleCloseModal();
                    mutate(`${process.env.NEXT_PUBLIC_PLAYER}/getall`);
                }
            })
            .catch(err => {
                toast.error("Error deleting player");
                console.error(err);
            });
    };

    const handleCloseModal = () => {
        setPlayerId(0);
        setShowModalDelete(false);
    };

    return (
        <Modal
            show={showModalDelete}
            onHide={handleCloseModal}
            backdrop="static"
        >
            <Modal.Header closeButton>
                <Modal.Title>Delete Confirmation</Modal.Title>
            </Modal.Header>

            <Modal.Body>Do you want to delete Player ID = {id}?</Modal.Body>

            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>
                    Confirm Delete
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default DeletePlayerModal;