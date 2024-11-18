import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap';
import './clubStyles.scss';
import CreateModal from '../modals/yourclub/createMatchA';
import ViewModal from '../modals/yourclub/viewMatches';

interface Matches {
    matchesID: string; // Ensure this is a string
    startTime: string;
    endTime: string;
    // Add other relevant fields if needed
}

interface MatchesProps {
    data: Matches[];
    clubID?: number;
}

const Matches: React.FC<MatchesProps> = ({ data, clubID }) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalView, setShowModalView] = useState<boolean>(false);
    const [selectedMatchID, setSelectedMatchID] = useState<string | undefined>(undefined);

    const handleAddMatch = () => {
        setShowModalCreate(true); // Open the modal for creating a new match
    };

    const handleViewMatch = (matchID: string) => {
        setSelectedMatchID(matchID);
        setShowModalView(true); // Open the modal for viewing match details
    };

    return (
        <div className="matches-container container">
            <h3>Matches</h3>
            <Button className="add-button" onClick={handleAddMatch}>
                Add Match
            </Button>
            {data.length === 0 ? (
                <div className="empty-state">
                    <p>No matches scheduled.</p>
                </div>
            ) : (
                <Table striped bordered hover className="mt-3 table-custom">
                    <thead>
                        <tr>
                            <th>Match ID</th>
                            <th>Start Time</th>
                            <th>End Time</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((match) => (
                            <tr key={match.matchesID}>
                                <td>{match.matchesID}</td>
                                <td>{match.startTime}</td>
                                <td>{match.endTime}</td>
                                <td>
                                    <Button
                                        className="view-button"
                                        onClick={() => handleViewMatch(match.matchesID)} // Ensure matchesID is a string
                                    >
                                        View
                                    </Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            )}
            {clubID !== undefined && (
                <CreateModal
                    showModalCreate={showModalCreate}
                    setShowModalCreate={setShowModalCreate}
                    clubID={clubID}
                />
            )}
            {clubID !== undefined && (
                <ViewModal
                    showModalView={showModalView}
                    setShowModalView={setShowModalView}
                    matchID={selectedMatchID} // This should be a string
                />
            )}
        </div>
    );
};

export default Matches;