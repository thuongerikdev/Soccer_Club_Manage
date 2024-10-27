import { Button } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import Link from 'next/link';
import CreateModal from '../modals/users/createUser.modal';
import DeleteModal from '../modals/users/deleteUser.modal';
import UpdateModals from '../modals/users/updateUser.modal';
import { useState } from 'react';

interface IProps {
    users: IUser[];
}

const UserTable = (props: IProps) => {
    const { users } = props;
    const [showModalCreate, setShowModalCreate] = useState<boolean>(false);
    const [showModalUpdate, setShowModalUpdate] = useState<boolean>(false);
    const [showModalDelete, setShowModalDelete] = useState<boolean>(false);
    const [user, setUser] = useState<IUser | null>(null);
    const [userId, setUserId] = useState<number>(0);

    // Các hàm mở modal đã điều chỉnh
    const handleUpdate = (item: IUser) => {
        setUser(item);
        if (!showModalUpdate) setShowModalUpdate(true);
    };

    const handleDelete = (item: number) => {
        setUserId(item);
        if (!showModalDelete) setShowModalDelete(true);
    };

    const handleCreate = () => {
        if (!showModalCreate) setShowModalCreate(true);
    };
   

    return (
        <>
            <div className='mb-3' style={{ display: "flex", justifyContent: "space-between" }}>
                <h3>User Table</h3>
                <Button variant="secondary" onClick={() => handleCreate()}>Add New</Button>
            </div>

            <Table striped="columns">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Username</th>
                        <th>Name</th>
                        <th>Password</th>
                        <th>Email</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Gender</th>
                        <th>Phone</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {users?.map(item => (
                        <tr key={item.userId}>
                            <td>{item.userId}</td>
                            <td>{item.username}</td>
                            <td>{item.name}</td>
                            <td>{item.password}</td>
                            <td>{item.email}</td>
                            <td>{item.age}</td>
                            <td>{item.address}</td>
                            <td>{item.gender}</td>
                            <td>{item.phone}</td>
                            <td>
                                <Link href={`/blogs/${item.userId}`} className='btn btn-primary'>View</Link>
                                <Button variant='warning' className='mx-3' onClick={() => handleUpdate(item)}>Edit</Button>
                                <Button variant='danger' onClick={() => handleDelete(item.userId)}>Delete</Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <CreateModal
                showModalCreate={showModalCreate}
                setShowModalCreate={setShowModalCreate}
            />

            <UpdateModals
                showModalUpdate={showModalUpdate}
                setShowModalUpdate={setShowModalUpdate}
                user={user}
                setUser={setUser}
            />

            <DeleteModal
                showModalDelete={showModalDelete}
                setShowModalDelete={setShowModalDelete}
                userId={userId}
                setUserId={setUserId}
            />
        </>
    );
};

export default UserTable;
