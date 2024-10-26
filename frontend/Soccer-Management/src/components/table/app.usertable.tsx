import { Button } from 'react-bootstrap';
import Table from 'react-bootstrap/Table';
import Link from 'next/link';

interface IProps {
    user: IUser []
}

const UserTable = (props: IProps) => {

    const {user} = props


    return (
        <>
            <div className='mb-3'
                style={{ display: "flex", justifyContent: "space-between" }}>
                <h3>Table blogs</h3>
                <Button variant="secondary">
                    Add New
                </Button>

            </div>

            <Table striped="columns">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>username</th>
                        <th>name</th>
                        <th>password</th>
                        <th>email</th>
                        <th>age</th>
                        <th>address</th>
                        <th>gender</th>
                        <th>phone</th>
                        
                    </tr>
                </thead>
                <tbody>
                    {user?.map(item => {
                        return (

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

                                    <Link href={`/blogs/${item.userId}`} className='btn btn-primary'> View</Link>
                                    <Button variant='warning' className='mx-3'> Edit</Button>
                                    <Button variant='danger' > Delete</Button>


                                </td>
                            </tr>


                        )
                    })}


                </tbody>
            </Table>


        </>
    )


}
export default UserTable