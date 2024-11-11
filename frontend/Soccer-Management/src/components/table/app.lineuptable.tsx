import { Button } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import CreateModal from '../modals/lineups/createLineup.modal'; // Ensure you have this modal
import DeleteModal from '../modals/lineups/deleteLineup.modal'; // Ensure you have this modal
import UpdateModal from '../modals/lineups/updateLineup.modal'; // Ensure you have this modal
import { useEffect, useState } from 'react';
import { FaAd, FaEdit, FaTrash } from 'react-icons/fa';



interface IProps {
    lineUps: LineUp[];
}

const LineupsTable = ({ lineUps }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [lineUp, setLineUp] = useState<LineUp | null>(null);
    const [lineUpId, setLineUpId] = useState<number>(0);

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
    useEffect(() => {
        console.log(lineUp)
    }
, [])

    return (
        <div className="container mt-5">
            <div className='mb-3 d-flex justify-content-between align-items-center'>
                <h3>Lineups Table</h3>
                <Button variant="primary" onClick={handleCreate}>Add New Lineup</Button>
            </div>

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
                    {lineUps.map(item => (
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
                                {/* <Button
                                        variant='warning'
                                        className='mx-2 btn-sm'
                                        onClick={() => handleUpdate(item)}
                                    >
                                        <FaAd /> View
                                    </Button> */}
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
                    ))}
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