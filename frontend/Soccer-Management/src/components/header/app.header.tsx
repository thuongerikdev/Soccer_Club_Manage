'use client'
import Link from 'next/link';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import './appHeader.css'; // Đảm bảo import file CSS
import { RootState } from '@/lib/store';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect, useState } from 'react';
import { setUser, logout } from '@/lib/features/loginSlice'; // Đảm bảo import logout action
import { useRouter } from 'next/navigation';

const AppHeader = () => {
  const router = useRouter()
  const dispatch = useDispatch();
  const isAuthenticated = useSelector((state: RootState) => state.auth.isAuthenticated);
  const user = useSelector((state: RootState) => state.auth.user);
  const [showModal, setShowModal] = useState(false); // State để quản lý modal

  useEffect(() => {
    const storedLoginState = localStorage.getItem('isAuthenticated');
    const storedUser = localStorage.getItem('user');

    if (storedLoginState && storedUser) {
      dispatch(setUser({ user: JSON.parse(storedUser), isAuthenticated: JSON.parse(storedLoginState) }));
    }
  }, [dispatch]);

  const handleLogout = () => {
    dispatch(logout()); // Gọi action logout để cập nhật trạng thái
    router.push('/login')
    localStorage.removeItem('user'); // Xóa thông tin người dùng khỏi localStorage
    localStorage.removeItem('isAuthenticated'); // Xóa trạng thái đăng nhập
    setShowModal(false);
     // Đóng modal
  };

  return (
    <>
      <Navbar className="custom-navbar" data-bs-theme="light">
        <Container>
          <Navbar.Brand>
            <Link href={'/'} className='navbar-brand'> SoccerManage</Link>
          </Navbar.Brand>
          <Nav className="me-auto">
            <Link href={'/users'} className='nav-link'> Users</Link>
            <Link href={'/players'} className='nav-link'> Players</Link>
            <Link href={'/club'} className='nav-link'> Club</Link>
            <Link href={'/lineup'} className='nav-link'> Line Up</Link>
            <Link href={'/playerLineup'} className='nav-link'> PlayerLineUp</Link>
            <Link href={'/matches'} className='nav-link'> Matches</Link>
          </Nav>
          <Nav>
            {isAuthenticated ? (
              <>
                <span className='nav-link'>Welcome, {user?.name}</span>
                <Button variant="link" className='nav-link' onClick={() => setShowModal(true)}>Log out</Button>
              </>
            ) : (
              <Link href={'/login'} className='nav-link'> Login</Link>
            )}
          </Nav>
        </Container>
      </Navbar>

      {/* Modal xác nhận đăng xuất */}
      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Confirm Logout</Modal.Title>
        </Modal.Header>
        <Modal.Body>Are you sure you want to log out?</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Cancel
          </Button>
          <Button variant="primary" onClick={handleLogout}>
            Log out
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default AppHeader;