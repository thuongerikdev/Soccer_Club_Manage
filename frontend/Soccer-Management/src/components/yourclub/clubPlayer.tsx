import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap';
import './clubStyles.scss';
import CreateModal from '../modals/players/createyourplayer.modal';
import UpdateModals from '../modals/yourclub/viewPlayer.modal';

interface PlayerProps {
    data: IPlayer[];
    clubID?: number;
    isOwner: boolean | string | undefined;  // Add isOwner prop
}

const Player: React.FC<PlayerProps> = ({ data, clubID, isOwner }) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [player, setPlayer] = useState<IPlayer | null>(null);

    const handleAddPlayer = () => {
        if (!isOwner) {
            alert("You don't have permission to create a player.");
            return;
        }
        setShowModalCreate(true);
    };

    const handleUpdate = (item: IPlayer) => {
        setPlayer(item);
        setShowModalUpdate(true);
    };

    // Lọc danh sách cầu thủ có playerStatus = 1
    const activePlayers = data.filter(player => player.playerStatus === 1);

    return (
        <div className="player-container container">
            <h3>Players</h3>
            {isOwner && (
                <Button className="add-button" onClick={handleAddPlayer}>
                    Create Player
                </Button>
            )}
            {activePlayers.length === 0  ? (
                <div className="empty-state">
                    <p>No players available.</p>
                </div>
            ) : (
                <Table striped bordered hover className="mt-3 table-custom">
                    <thead>
                        <tr>
                            <th>Player Name</th>
                            <th>Player Position</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {activePlayers.map((player) => (
                            <tr key={player.playerID}>
                                <td>{player.playerName}</td>
                                <td>{player.playerPosition}</td>
                                <td>
                                    <Button 
                                        className="view-button"
                                        onClick={() => handleUpdate(player)}
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
            <UpdateModals
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                player={player}
                setPlayer={setPlayer}
                isOwner={isOwner}  // Truyền isOwner vào UpdateModals
            />
        </div>
    );
};

export default Player;