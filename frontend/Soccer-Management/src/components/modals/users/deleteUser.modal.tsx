import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalDelete: boolean;
    setShowModalDelete: (value: boolean) => void;
    userId: number;
    setUserId: (value: number) => void;
}

function DeleteModal(props: IProps) {
    const { showModalDelete, setShowModalDelete, userId, setUserId } = props;
    const [id , setId] = useState<number>(0)


    useEffect(() => {
        setId(userId)
    },[userId])

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_USER}/remove/${id}`, {
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
                    mutate(process.env.NEXT_PUBLIC_USER_GETALL);
                }
            });
    };

    const handleCloseModal = () => {
        setUserId(0);
        setShowModalDelete(false);
    };

    return (
        <>
            <Modal
                show={showModalDelete}
                onHide={() => (handleCloseModal)}
                backdrop="static"
            >
                <Modal.Header closeButton>
                    <Modal.Title>Delete Confirmation</Modal.Title>
                </Modal.Header>

                <Modal.Body>Do you want to delete User ID = {id}?</Modal.Body>

                <Modal.Footer>
                    <Button variant="secondary" onClick={() => handleCloseModal()}>
                        Close
                    </Button>
                    <Button variant="primary" onClick={() => handleSubmit()}>
                        Confirm Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </>

    );
}

export default DeleteModal;
