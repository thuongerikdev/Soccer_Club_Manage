import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';

interface IProps {
    showModalDelete: boolean;
    setShowModalDelete: (value: boolean) => void;
    matchId: number;
    setMatchId: (value: number) => void;
}

function DeleteMatchModal(props: IProps) {
    const { showModalDelete, setShowModalDelete, matchId, setMatchId } = props;
    const [id, setId] = useState<number>(0);

    useEffect(() => {
        setId(matchId);
    }, [matchId]);

    const handleSubmit = () => {
        fetch(`${process.env.NEXT_PUBLIC_MATCHES}/deleteMatches/${matchId}`, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: 'DELETE',
        })
            .then(res => {
                if (!res.ok) {
                    throw new Error(`Error: ${res.statusText}`);
                }
                return res.json();
            })
            .then(res => {
                console.log("Response from delete:", res); // Log the response for debugging
               
                    toast.success('Delete successful');
                    handleCloseModal();
                    mutate(`${process.env.NEXT_PUBLIC_MATCHES}/getAllMatches`); // Adjust endpoint if needed
               
            })
           
    };

    const handleCloseModal = () => {
        setMatchId(0);
        setShowModalDelete(false);
    };

    return (
        <>
            <Modal
                show={showModalDelete}
                onHide={handleCloseModal}
                backdrop="static"
            >
                <Modal.Header closeButton>
                    <Modal.Title>Delete Confirmation</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    Do you want to delete MATCH ID = {id}?
                </Modal.Body>

                <Modal.Footer>
                    <Button variant="secondary" onClick={handleCloseModal}>
                        Close
                    </Button>
                    <Button variant="danger" onClick={handleSubmit}>
                        Confirm Delete
                    </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default DeleteMatchModal;
