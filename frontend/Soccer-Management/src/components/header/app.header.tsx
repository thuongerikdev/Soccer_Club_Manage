'use client'
import Link from 'next/link';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import './appHeader.css'; // Đảm bảo import file CSS

const AppHeader = () => {
  return (
    <>
      <Navbar className="custom-navbar" data-bs-theme="light">
        <Container>
          <Navbar.Brand>
            <Link href={'/'} className='navbar-brand'> SoccerManage</Link>
           </Navbar.Brand>
          <Nav className="me-auto">
            <Link href={'/users'} className='nav-link'> User</Link>
            <Link href={'/blogs'} className='nav-link'> Blog</Link>
            <Link href={'/login'} className='nav-link'> Login</Link>
          </Nav>
        </Container>
      </Navbar>
    </>
  );
}

export default AppHeader;