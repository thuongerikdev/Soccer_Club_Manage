import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { toast } from 'react-toastify';
import { mutate } from 'swr';
import { useRouter } from "next/navigation";
interface IProps {
    showModalDelete: boolean;
    setShowModalDelete: (value: boolean) => void;
    lineUpId: number;
    setLineUpId: (value: number) => void;
    clubID : number
}

function DeleteLineUpModal(props: IProps) {
    const router = useRouter();
    const { showModalDelete, setShowModalDelete, lineUpId,clubID ,setLineUpId } = props;
    const [id, setId] = useState<number>(0);

    useEffect(() => {
        setId(lineUpId);
    }, [lineUpId]);

    const handleSubmit = async () => {
        try {
            const response = await fetch(`${process.env.NEXT_PUBLIC_LINEUP}/deleteLineUp/${id}`, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "DELETE",
            });

            if (response.ok) {
                // Check if the response body contains data
                const res = await response.json().catch(() => null); // Avoid parsing errors
                if (res) {
                    toast.success("Delete successful");
                    handleCloseModal();
                    router.push(`/yourclub/${clubID}`);
                } else {
                    toast.error("No data returned after deletion.");
                }
            } else {
                const errorRes = await response.text();
                toast.error(`Error deleting lineup: ${errorRes || 'Unknown error'}`);
            }
        } catch (err) {
            toast.error("Error deleting lineup: " + err);
            console.error("Error details:", err);
        }
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
