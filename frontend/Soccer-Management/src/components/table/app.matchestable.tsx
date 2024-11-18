import { Button, Table, Form } from 'react-bootstrap';
import CreateModal from '../modals/matches/creatematch.modal';
import DeleteModal from '../modals/matches/deletematch.modal';
import UpdateModal from '../modals/matches/updatematch.modal';
import { useState } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';

interface IProps {
    matches: Matches[]; // Define the Matches interface accordingly
}

const MatchesTable = ({ matches }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [match, setMatch] = useState<Matches | null>(null);
    const [matchId, setMatchId] = useState<number>(0);
    const [searchId, setSearchId] = useState<number | ''>('');

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

    // Filter matches based on search ID
    const filteredMatches = matches.filter(match => 
        searchId === '' || match.matchesID === searchId
    );

    return (
        <div className="container mt-5">
            <div className='mb-3 d-flex justify-content-between align-items-center'>
                <h3>Matches Table</h3>
                <Button variant="primary" onClick={handleCreate}>Add New Match</Button>
            </div>

            <Form className="mb-3">
                <Form.Group>
                    <Form.Label>Search by Match ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter match ID" 
                        value={searchId} 
                        onChange={(e) => setSearchId(e.target.value ? parseInt(e.target.value) : '')}
                    />
                </Form.Group>
            </Form>

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
                    
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredMatches.length > 0 ? filteredMatches.map(item => {
                        if (item.matchesID === undefined) {
                            console.error('Invalid match item without matchesId:', item);
                            return null; // Skip rendering this item
                        }
                        return (
                            <tr key={`match-${item.matchesID}`}>
                                <td>{item.matchesID}</td>
                                <td>{item.matchesName}</td>
                                <td>{item.matchesDescription}</td>
                                <td>{item.tournamentID}</td>
                                <td>{item.stadium}</td>
                                <td>{new Date(item.startTime).toLocaleString()}</td>
                                <td>{new Date(item.endTime).toLocaleString()}</td>
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
                                            onClick={() => handleDelete(item.matchesID)}
                                        >
                                            <FaTrash /> Delete
                                        </Button>
                                    </div>
                                </td>
                            </tr>
                        );
                    }) : (
                        <tr>
                            <td colSpan={10} className="text-center">No matches found</td>
                        </tr>
                    )}
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