import React, { useState } from 'react';
import { Table, Button } from 'react-bootstrap'; // Import Table and Button from React Bootstrap
import './clubStyles.scss'; // Import the consolidated SCSS
import useSWR from 'swr';
import CreateModal from '../modals/players/createyourplayer.modal';
import UpdateModals from '../modals/yourclub/viewPlayer.modal';


interface PlayerProps {
    data: IPlayer[]; // Initial data (could be preloaded)
    clubID?: number;
    isOwner: boolean | string | undefined;
}

// SWR Fetcher function
const fetcher = (url: string) => fetch(url).then((res) => res.json());

const Player: React.FC<PlayerProps> = ({ clubID, isOwner }) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [player, setPlayer] = useState<IPlayer | null>(null);

    // Use SWR for data fetching
    const { data, error, mutate } = useSWR(
        clubID ? `${process.env.NEXT_PUBLIC_PLAYER}/getPlayerClub/${clubID}` : null,
        fetcher,
        { revalidateOnFocus: false }
    );

    // Ensure player data is available and filter active players
    const players = data?.data || [];
    const activePlayers = players.filter((player: IPlayer) => player.playerStatus === 1);

    const handleAddPlayer = () => {
        setShowModalCreate(true);
    };

    const handleUpdate = (item: IPlayer) => {
        setPlayer(item);
        setShowModalUpdate(true);
    };

    return (
        <div className="player-container container">
            <h3>Players</h3>
            {/* Create Player button */}
            <Button className="add-button" onClick={handleAddPlayer}>
                Create Player
            </Button>
            {error ? (
                <div className="error-state">
                    <p>Error loading players. Please try again later.</p>
                </div>
            ) : !data ? (
                <div className="loading-state">
                    <p>Loading...</p>
                </div>
            ) : activePlayers.length === 0 ? (
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
                        {activePlayers.map((player: IPlayer) => (
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
                onLineupCreated={() => mutate()} // Trigger revalidation

            />
            <UpdateModals
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                player={player}
                setPlayer={setPlayer}
                isOwner={isOwner} // Pass isOwner to UpdateModals
                onLineupCreated={() => mutate()} // Trigger revalidation
            />
        </div>
    );
};

export default Player;
