import { Button } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import CreateModal from '../modals/clubs/createClub.modal';
import DeleteModal from '../modals/clubs/deleteClub.modal';
import UpdateModals from '../modals/clubs/updateClub.modal';
import { useState } from 'react';
import { FaEye, FaEdit, FaTrash } from 'react-icons/fa';



interface IProps {
    clubs: IClub[];
}

const ClubTable = ({ clubs }: IProps) => {
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [club, setClub] = useState<IClub | null>(null);
    const [clubId, setClubId] = useState<number>(0);

    const handleUpdate = (item: IClub) => {
        setClub(item);
        setShowModalUpdate(true);
    };

    const handleDelete = (id: number) => {
        setClubId(id);
        setShowModalDelete(true);
    };

    const handleCreate = () => {
        setShowModalCreate(true);
    };

    return (
        <div className="container mt-5">
            <div className='mb-3 d-flex justify-content-between align-items-center'>
                <h3>Club Table</h3>
                <Button variant="primary" onClick={handleCreate}>Add New Club</Button>
            </div>

            <Table striped bordered hover responsive>
                <thead className="table-light">
                    <tr>
                        <th>ID</th>
                        <th>Club Name</th>
                        <th>Description</th>
                        <th>Logo</th>
                        <th>Banner</th>
                        <th>Boss ID</th>
                        <th>Budget</th>
                        <th>Level</th>
                        <th>Age</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {clubs.map(item => {
                        if (item.clubId === undefined) {
                            console.error('Invalid club item without clubId:', item);
                            return null; // Skip rendering this item
                        }
                        return (
                            <tr key={`club-${item.clubId}`}>
                                <td>{item.clubId}</td>
                                <td>{item.clubName}</td>
                                <td>{item.clubDescription}</td>
                                <td>
                                    {item.clubLogo ? (
                                        <img alt={item.clubName}
                                        src="/cardData2.png" style={{ width: '50px', borderRadius: '50%' }} />
                                    ) : null}
                                </td>
                                <td>
                                    {item.clubBanner ? (
                                        <img alt={item.clubName} 
                                        src="/cardData1.png"
                                        style={{ width: '100px', borderRadius: '5%' }} />
                                    ) : null}
                                </td>
                                <td>{item.userId}</td>
                                <td>{item.budget}</td>
                                <td>{item.clubLevel}</td>
                                <td>{item.clubAge}</td>
                                <td>
                                    <div className="d-flex justify-content-around">
                                        <Button
                                            variant='warning'
                                            className='mx-2 btn-sm button-equal'
                                            onClick={() => handleUpdate(item)}
                                        >
                                            <FaEdit /> Update
                                        </Button>
                                        <Button
                                            variant='danger'
                                            className='btn-sm button-equal'
                                            onClick={() => handleDelete(item.clubId)}
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

            <UpdateModals
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                club={club}
                setClub={setClub}
            />

            <DeleteModal
                showModalDelete={showModalDelete}
                setShowModalDelete={setShowModalDelete}
                clubId={clubId}
                setCLubId={setClubId}
            />
        </div>
    );
};

export default ClubTable;