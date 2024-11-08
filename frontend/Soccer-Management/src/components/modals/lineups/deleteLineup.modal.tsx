import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalDelete: boolean;
    setShowModalDelete: (value: boolean) => void;
    lineUpId: number;
    setLineUpId: (value: number) => void;
}

function DeleteLineUpModal(props: IProps) {
    const { showModalDelete, setShowModalDelete, lineUpId, setLineUpId } = props;
    const [id, setId] = useState<number>(0);

    useEffect(() => {
        setId(lineUpId);
    }, [lineUpId]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_LINEUP}/remove/${id}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "DELETE",
        })
            .then(res => res.json())
            .then(res => {
                if (res) {
                    toast.success("Delete successful");
                    handleCloseModal();
                    mutate(`${process.env.NEXT_PUBLIC_LINEUP}/getall`);
                }
            })
            .catch(err => {
                toast.error("Error deleting lineup: " + err.message);
                console.error("Error details:", err);
            });
    };

    const handleCloseModal = () => {
        setLineUpId(0);
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

            <Modal.Body>Do you want to delete Lineup ID = {id}?</Modal.Body>

            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
                <Button variant="danger" onClick={handleSubmit}>
                    Confirm Delete
                </Button>
            </Modal.Footer>
        </Modal>
    );
}

export default DeleteLineUpModal;