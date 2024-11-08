import { Button } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import CreateModal from '../modals/matches/creatematch.modal'; // Ensure you have this modal
import DeleteModal from '../modals/matches/deletematch.modal'; // Ensure you have this modal
import UpdateModal from '../modals/matches/updatematch.modal'; // Ensure you have this modal
import { useState } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';

interface IProps {
    matches: Matches[]; // Define the IMatch interface accordingly
}

const MatchesTable = ({ matches }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [match, setMatch] = useState<Matches | null>(null);
    const [matchId, setMatchId] = useState<number>(0);

    const handleUpdate = (item: Matches) => {
        setMatch(item);
        setShowModalUpdate(true);
    };

    const handleDelete = (id: number) => {
        setMatchId(id);
        setShowModalDelete(true);
    };

    const handleCreate = () => {
        setShowModalCreate(true);
    };

    return (
        <div className="container mt-5">
            <div className='mb-3 d-flex justify-content-between align-items-center'>
                <h3>Matches Table</h3>
                <Button variant="primary" onClick={handleCreate}>Add New Match</Button>
            </div>

            <Table striped bordered hover responsive>
                <thead className="table-light">
                    <tr>
                        <th>ID</th>
                        <th>Match Name</th>
                        <th>Description</th>
                        <th>Tournament ID</th>
                        <th>Stadium</th>
                        <th>Start Time</th>
                        <th>End Time</th>
                        <th>Team Win</th>
                        <th>Team Lose</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {matches.map(item => {
                        if (item.matchesId === undefined) {
                            console.error('Invalid match item without matchesId:', item);
                            return null; // Skip rendering this item
                        }
                        return (
                            <tr key={`match-${item.matchesId}`}>
                                <td>{item.matchesId}</td>
                                <td>{item.matchesName}</td>
                                <td>{item.matchesDescription}</td>
                                <td>{item.tournamentId}</td>
                                <td>{item.stadium}</td>
                                <td>{new Date(item.startTime).toLocaleString()}</td>
                                <td>{new Date(item.endTime).toLocaleString()}</td>
                                <td>{item.teamWin}</td>
                                <td>{item.teamLose}</td>
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
                                            onClick={() => handleDelete(item.matchesId)}
                                        >
                                            <FaTrash /> Delete
                                        </Button>
                                    </div>
                                </td>
                            </tr>
                        );
                    })}
                </tbody>
            </Table>

            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
            />

            <UpdateModal
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                match={match}
                setMatch={setMatch}
            />

            <DeleteModal
                showModalDelete={showModalDelete}
                setShowModalDelete={setShowModalDelete}
                matchId={matchId}
                setMatchId={setMatchId}
            />
        </div>
    );
};

export default MatchesTable;