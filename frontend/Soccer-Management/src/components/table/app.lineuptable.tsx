import { Button, Table, Form } from 'react-bootstrap';
import CreateModal from '../modals/lineups/createLineup.modal';
import DeleteModal from '../modals/lineups/deleteLineup.modal';
import UpdateModal from '../modals/lineups/updateLineup.modal';
import { useEffect, useState } from 'react';
import { FaEdit, FaTrash } from 'react-icons/fa';

interface IProps {
    lineUps: LineUp[];
}

const LineupsTable = ({ lineUps }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [lineUp, setLineUp] = useState<LineUp | null>(null);
    const [lineUpId, setLineUpId] = useState<number>(0);
    const [searchId, setSearchId] = useState<number | ''>('');

    const handleUpdate = (item: LineUp) => {
        setLineUp(item);
        setShowModalUpdate(true);
    };

    const handleDelete = (id: number) => {
        setLineUpId(id);
        setShowModalDelete(true);
    };

    const handleCreate = () => {
        setShowModalCreate(true);
    };

    // Filter lineups based on search ID
    const filteredLineUps = lineUps.filter(lineup => 
        searchId === '' || lineup.lineUpId === searchId
    );

    useEffect(() => {
        console.log(lineUp);
    }, [lineUp]);

    return (
        <div className="container mt-5">
            <div className='mb-3 d-flex justify-content-between align-items-center'>
                <h3>Lineups Table</h3>
                <Button variant="primary" onClick={handleCreate}>Add New Lineup</Button>
            </div>

            <Form className="mb-3">
                <Form.Group>
                    <Form.Label>Search by Lineup ID</Form.Label>
                    <Form.Control 
                        type="number" 
                        placeholder="Enter lineup ID" 
                        value={searchId} 
                        onChange={(e) => setSearchId(e.target.value ? parseInt(e.target.value) : '')}
                    />
                </Form.Group>
            </Form>

            <Table striped bordered hover responsive>
                <thead className="table-light">
                    <tr>
                        <th>ID</th>
                        <th>Match ID</th>
                        <th>Club ID</th>
                        <th>Lineup Name</th>
                        <th>Lineup Type</th>
                        <th>Match Type</th>
                        <th>Stadium Background</th>
                        <th>Created At</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {filteredLineUps.length > 0 ? filteredLineUps.map(item => (
                        <tr key={`lineup-${item.lineUpId}`}>
                            <td>{item.lineUpId}</td>
                            <td>{item.matchId}</td>
                            <td>{item.clubId}</td>
                            <td>{item.lineUpName}</td>
                            <td>{item.lineUpType}</td>
                            <td>{item.matchType}</td>
                            <td>
                                {item.stadiumBackGround ? (
                                    <img 
                                        alt={item.lineUpName} 
                                        src={item.stadiumBackGround} 
                                        style={{ width: '100px', borderRadius: '5%' }} 
                                    />
                                ) : (
                                    <span>No Image</span>
                                )}
                            </td>
                            <td>{new Date(item.createAt).toLocaleString()}</td>
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
                                        onClick={() => handleDelete(item.lineUpId)}
                                    >
                                        <FaTrash /> Delete
                                    </Button>
                                </div>
                            </td>
                        </tr>
                    )) : (
                        <tr>
                            <td colSpan={9} className="text-center">No lineups found</td>
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
                lineUp={lineUp}
                setLineUp={setLineUp}
            />

            <DeleteModal
                showModalDelete={showModalDelete}
                setShowModalDelete={setShowModalDelete}
                lineUpId={lineUpId}
                setLineUpId={setLineUpId}
            />
        </div>
    );
};

export default LineupsTable;