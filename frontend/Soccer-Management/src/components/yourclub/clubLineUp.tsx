import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap';
import { useRouter } from 'next/navigation';
import CreateModal from '../modals/lineups/createyourLineup.modal';

interface LineUp {
    lineUpID: number;
    lineUpName: string;
    playerNumber: number;
    lineUpType: string;
}

interface LineupProps {
    data: LineUp[];
    clubID?: number;
    isOwner: boolean | string | undefined;  // Add isOwner prop
}

const Lineup: React.FC<LineupProps> = ({ data, clubID, isOwner }) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [lineupData, setLineupData] = useState<LineUp[]>(data);
    const router = useRouter();

    const handleAddLineup = () => {
        if (!isOwner) {
            alert("You don't have permission to create a lineup.");
            return;
        }
        setShowModalCreate(true);
    };

    const handleViewLineup = (lineupId: number) => {
        router.push(`/yourclub/${clubID}/${lineupId}`);
    };

    const refreshLineupData = async () => {
        try {
            const response = await fetch(`${process.env.NEXT_PUBLIC_LINEUP}/getLineUpClub/${clubID}`);
            const data = await response.json();
            setLineupData(data.data);
        } catch (error) {
            console.error("Error fetching lineup data:", error);
        }
    };

    return (
        <div className="lineup-container-1 container">
            <h3>Lineup</h3>
            {isOwner && (
                <Button className="add-button" onClick={handleAddLineup}>
                    Create Lineup
                </Button>
            )}
            {lineupData.length === 0 ? (
                <div className="empty-state">
                    <p>No players in the lineup.</p>
                </div>
            ) : (
                <Table striped bordered hover className="mt-3 table-custom">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Lineup Name</th>
                            <th>Player Number</th>
                            <th>Lineup Type</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {lineupData.map((lineup) => (
                            <tr key={lineup.lineUpID}>
                                <td>{lineup.lineUpID}</td>
                                <td>{lineup.lineUpName}</td>
                                <td>{lineup.playerNumber}</td>
                                <td>{lineup.lineUpType}</td>
                                <td>
                                    <Button
                                        className="view-button"
                                        onClick={() => handleViewLineup(lineup.lineUpID)}
                                    >
                                        View
                                    </Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            )}
            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
                clubID={clubID}
                onLineupCreated={refreshLineupData}
            />
        </div>
    );
};
export default Lineup;
