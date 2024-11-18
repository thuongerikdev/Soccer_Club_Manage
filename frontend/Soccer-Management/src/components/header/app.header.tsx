'use client';
import Link from 'next/link';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import './appHeader.scss'; // Ensure this file exists
import { RootState } from '@/lib/store';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect, useState } from 'react';
import { setUser, logout } from '@/lib/features/loginSlice';
import { useRouter } from 'next/navigation';

interface AppHeaderProps {
  userRole: string | null;
  loading: boolean;
}

const AppHeader = ({ userRole, loading }: AppHeaderProps) => {
  const router = useRouter();
  const dispatch = useDispatch();
  const isAuthenticated = useSelector((state: RootState) => state.auth.isAuthenticated);
  const user = useSelector((state: RootState) => state.auth.user);
  const [showModal, setShowModal] = useState(false);

  useEffect(() => {
    const storedLoginState = localStorage.getItem('isAuthenticated');
    const storedUser = localStorage.getItem('user');

    if (storedLoginState && storedUser) {
      dispatch(setUser({ user: JSON.parse(storedUser), isAuthenticated: JSON.parse(storedLoginState) }));
    }
  }, [dispatch]);

  const handleLogout = () => {
    dispatch(logout());
    localStorage.removeItem('user');
    localStorage.removeItem('isAuthenticated');
    setShowModal(false);
    router.push('/login');
  };

  return (
    <>
      <Navbar className="custom-navbar" data-bs-theme="light">
        <div className="nav-container">
          <Navbar.Brand>
            <Link href={'/'} className='navbar-brand'> SoccerManage</Link>
          </Navbar.Brand>
          <Nav className="nav-links">
            {loading ? (
              <span className="nav-link">Loading...</span>
            ) : (
              <>
                {isAuthenticated && userRole !== 'clubmanager' && (
                  <>
                    <Link href={'/users'} className='nav-link'> Users</Link>
                    <Link href={'/players'} className='nav-link'> Players</Link>
                    <Link href={'/club'} className='nav-link'> Club</Link>
                    <Link href={'/lineup'} className='nav-link'> Line Up</Link>
                    <Link href={'/matches'} className='nav-link'> Matches</Link>
                  </>
                )}
                <Link href={'/yourclub'} className='nav-link'> Your Club</Link>
                <Link href={'/otherClub'} className='nav-link'>Other Club</Link>
              </>
            )}
          </Nav>
          <Nav>
            {isAuthenticated ? (
              <>
                <span className='nav-link'>Welcome, {user?.name}</span>
                <Button variant="button" className='nav-link logout-button' onClick={() => setShowModal(true)}>Log out</Button>
              </>
            ) : (
              <Link href={'/login'} className='nav-link'> Login</Link>
            )}
          </Nav>
        </div>
      </Navbar>

      {/* Logout Confirmation Modal */}
      <Modal show={showModal} onHide={() => setShowModal(false)}>
        <Modal.Header closeButton>
          <Modal.Title>Confirm Logout</Modal.Title>
        </Modal.Header>
        <Modal.Body>Are you sure you want to log out?</Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={() => setShowModal(false)}>
            Cancel
          </Button>
          <Button variant="secondary" onClick={handleLogout}>Log out</Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}

export default AppHeader;