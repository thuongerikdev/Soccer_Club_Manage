import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap';
import './clubStyles.scss';
import CreateModal from '../modals/matches/createyourmatches.modal';
import ViewModal from '../modals/yourclub/viewMatches'; // Ensure the path is correct

interface MatchesProps {
    data: Matches[]; // Assuming Matches is defined elsewhere
    clubID?: number;
}

const MatcheB: React.FC<MatchesProps> = ({ data, clubID }) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalView, setShowModalView] = useState<boolean>(false);
    const [selectedMatchID, setSelectedMatchID] = useState<string | undefined>(undefined);

    const handleAddMatch = () => {
        setShowModalCreate(true); // Open the modal for creating a new match
    };

    const handleViewMatch = (matchID: number) => {
        setSelectedMatchID(matchID.toString()); // Convert matchID to string
        setShowModalView(true); // Open the modal for viewing match details
    };

    return (
        <div className="matches-container container">
            <h3>Matches</h3>
            {/* <Button className="add-button" onClick={handleAddMatch}>
                Add Match
            </Button> */}
            {data.length === 0 ? (
                <div className="empty-state">
                    <p>No matches scheduled.</p>
                </div>
            ) : (
                <Table striped bordered hover className="mt-3 table-custom">
                    <thead>
                        <tr>
                            <th>Match ID</th>
                            <th>Team A</th>
                            <th>Start Time</th>
                            <th>End Time</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((match) => (
                            <tr key={match.matchesID}>
                                <td>{match.matchesID}</td>
                                <td>{match.teamA}</td>
                                <td>{match.startTime}</td>
                                <td>{match.endTime}</td>
                                <td>
                                    <Button 
                                        className="view-button"
                                        onClick={() => handleViewMatch(match.matchesID)} // Pass the match ID
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
            />
            <ViewModal
                showModalView={showModalView}
                setShowModalView={setShowModalView}
                matchID={selectedMatchID}
            />
        </div>
    );
};

export default MatcheB;