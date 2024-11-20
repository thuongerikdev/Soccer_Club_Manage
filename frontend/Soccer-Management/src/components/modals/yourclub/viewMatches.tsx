import React, { useEffect, useState } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { mutate } from 'swr';

interface ViewModalProps {
    showModalView: boolean;
    setShowModalView: (show: boolean) => void;
    clubID?: number;
    matchID?: string; // Pass the match ID to fetch details
}

const ViewModal: React.FC<ViewModalProps> = ({ showModalView, setShowModalView, matchID }) => {
    const [matchDetails, setMatchDetails] = useState<any>(null); // Use appropriate type
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        const fetchMatchDetails = async () => {
            if (matchID) {
                setLoading(true);
                try {
                    const response = await fetch(`${process.env.NEXT_PUBLIC_MATCHES}/getMatches/${matchID}`); // Adjust the API endpoint
                    if (!response.ok) {
                        throw new Error('Failed to fetch match details');
                    }
                    const data = await response.json();
                    setMatchDetails(data.data);
                } catch (error) {
                    console.error(error);
                } finally {
                    setLoading(false);
                }
            }
        };

        fetchMatchDetails();
    }, [matchID]);

    const handleDeleteMatches = async (id: number) => {
        if (window.confirm("Are you sure you want to delete this player?")) {
            setLoading(true); // Bắt đầu trạng thái loading
            try {
                const response = await fetch(`${process.env.NEXT_PUBLIC_MATCHES}/deleteMatches/${id}`, {
                    method: 'DELETE', // Đảm bảo sử dụng phương thức DELETE
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });
    
                if (!response.ok) {
                    const errorData = await response.json(); // Lấy thông tin lỗi từ phản hồi
                    throw new Error(errorData.message || 'Failed to delete the match');
                }
    
                const data = await response.json();

                mutate(`${process.env.NEXT_PUBLIC_MATCHES}/TeamA/${id}`);
                alert('Match deleted successfully!'); // Thông báo thành công
                
            } catch (error) {
                console.error(error);
                alert(`Error: ${error}`);
            } finally {
                setLoading(false); 
            }
        }
    }

    return (
        <Modal show={showModalView} onHide={() => setShowModalView(false)}>
            <Modal.Header closeButton>
                <Modal.Title>Match Details</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {loading ? (
                    <p>Loading...</p>
                ) : (
                    matchDetails && (
                        <div>
                            <h5>Match ID: {matchDetails.matchesID}</h5>
                            <p>Team A: {matchDetails.teamA}</p>
                            <p>Team B: {matchDetails.teamB}</p>
                            <p>Start Time: {matchDetails.startTime}</p>
                            <p>End Time: {matchDetails.endTime}</p>
                        </div>
                    )
                )}
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => setShowModalView(false)}>
                    Close
                </Button>
                <Button variant="secondary" onClick={() => handleDeleteMatches(matchDetails.matchesID)}>
                    delete
                </Button>
            </Modal.Footer>
        </Modal>
    );
};

export default ViewModal;