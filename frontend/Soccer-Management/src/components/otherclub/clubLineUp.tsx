import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap';
import { useRouter } from 'next/navigation';
import useSWR from 'swr';
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
}

// Fetcher function for SWR
const fetcher = (url: string) => fetch(url).then((res) => res.json());

const Lineup: React.FC<LineupProps> = ({ clubID }) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const router = useRouter();

    // Use SWR for data fetching
    const { data, error, mutate } = useSWR(
        clubID ? `${process.env.NEXT_PUBLIC_LINEUP}/getLineUpClub/${clubID}` : null,
        fetcher,
        { revalidateOnFocus: false }
    );

    const handleAddLineup = () => {
        setShowModalCreate(true);
    };

    const handleViewLineup = (lineupId: number) => {
        console.log('Viewing lineup with ID:', lineupId);
        router.push(`/yourclub/${clubID}/${lineupId}`);
    };

    const lineupData = data?.data || [];

    return (
        <div className="lineup-container container">
            <h3>Lineup</h3>
            <Button className="add-button" onClick={handleAddLineup}>
                Create Lineup
            </Button>
            {error ? (
                <div className="error-state">
                    <p>Error loading lineup data. Please try again later.</p>
                </div>
            ) : !data ? (
                <div className="loading-state">
                    <p>Loading...</p>
                </div>
            ) : lineupData.length === 0 ? (
                <div className="empty-state">
                    <p>No players in the lineup.</p>
                </div>
            ) : (
                <Table striped bordered hover className="mt-3 table-custom">
                    <thead>
                        <tr>
                            <th>Lineup Name</th>
                            <th>Player Number</th>
                            <th>Lineup Type</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {lineupData.map((lineup: LineUp) => (
                            <tr key={lineup.lineUpID}>
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
                onLineupCreated={() => mutate()} // Trigger revalidation
            />
        </div>
    );
};

export default Lineup;
