import { Button } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import CreateModal from '../modals/players/createPlayer.modal';
import DeleteModal from '../modals/players/deletePlayer.modal';
import UpdateModal from '../modals/players/updatePlayer.modal';
import { useState } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';


interface IProps {
    players: IPlayer[];
}

const PlayerTable = ({ players }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [player, setPlayer] = useState<IPlayer | null>(null);
    const [playerId, setPlayerId] = useState<number>(0);

    const handleUpdate = (item: IPlayer) => {
        setPlayer(item);
        setShowModalUpdate(true);
    };

    const handleDelete = (id: number) => {
        setPlayerId(id);
        setShowModalDelete(true);
    };

    const handleCreate = () => {
        setShowModalCreate(true);
    };

    return (
        <div className="container mt-5">
            <div className='mb-3 d-flex justify-content-between align-items-center'>
                <h3>Player Table</h3>
                <Button variant="primary" onClick={handleCreate}>Add New Player</Button>
            </div>
            <div className="card">
                <Table striped bordered hover responsive>
                    <thead className="table-light">
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Position</th>
                            <th>Nationality</th>
                            <th>Image</th>
                            <th>Age</th>
                            <th>Value</th>
                            <th>Health</th>
                            <th>Skill</th>
                            <th>Salary</th>
                            <th>Club</th>
                            <th>Shirt Number</th>
                            <th>Status</th>
                            <th>Leg</th>
                            <th>Height</th>
                            <th>Weight</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {players.map(item => (
                            <tr key={item.playerId} className="table-row">
                                <td>{item.playerId}</td>
                                <td>{item.playerName}</td>
                                <td>{item.playerPosition}</td>
                                <td>{item.playerNationality}</td>
                                <td>
                                    {item.playerImage ? (
                                        <img 
                                            alt={item.playerName} 
                                            src="/cardData10.png"
                                            style={{ width: '50px', borderRadius: '50%' }} 
                                        />
                                    ) : (
                                        <img 
                                            alt="Placeholder" 
                                            src="/cardData12.png" // Replace with your placeholder image path
                                            style={{ width: '50px', borderRadius: '50%' }} 
                                        />
                                    )}
                                </td>
                                <td>{item.playerAge}</td>
                                <td>{item.playerValue}</td>
                                <td>{item.playerHealth}</td>
                                <td>{item.playerSkill}</td>
                                <td>{item.playerSalary}</td>
                                <td>{item.clubId}</td>
                                <td>{item.shirtnumber}</td>
                                <td>{item.playerStatus}</td>
                                <td>{item.leg}</td>
                                <td>{item.height} cm</td>
                                <td>{item.weight} kg</td>
                                <td>
                                    <div className="d-flex justify-content-around">
                                        <Button 
                                            variant='warning' 
                                            className='mx-2 btn-sm' 
                                            onClick={() => handleUpdate(item)}
                                        >
                                            <FaEdit /> Update
                                        </Button>
                                        <Button 
                                            variant='danger' 
                                            className='btn-sm' 
                                            onClick={() => handleDelete(item.playerId)}
                                        >
                                            <FaTrash /> Delete
                                        </Button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            </div>

            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
            />

            <UpdateModal
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                player={player}
                setPlayer={setPlayer}
            />

            <DeleteModal
                showModalDelete={showModalDelete}
                setShowModalDelete={setShowModalDelete}
                playerId={playerId}
                setPlayerId={setPlayerId}
            />
        </div>
    );
};

export default PlayerTable;