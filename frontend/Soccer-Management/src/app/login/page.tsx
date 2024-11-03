'use client';
import Image from "next/image";
import './page.scss';
import React, { useState, useEffect } from "react";
import { useRouter } from 'next/navigation';
import useSWR, { mutate } from 'swr';
import { useSelector, useDispatch } from 'react-redux'
import { login, logout } from "@/lib/features/loginSlice";
import { toast } from "react-toastify";
import { decodeToken } from "@/lib/decode/decodeToken";
import { saveToLocalStorage } from "@/lib/ReloginAction";
import withAuth from '../../components/middleware/withAuth';
import { RootState } from "@/lib/store";
import { Button, Container, Row, Col, Card, Spinner, Form } from 'react-bootstrap';

const LoginPage = () => {
  const dispatch = useDispatch();
  const router = useRouter();
  const isLoggedIn = useSelector((state: RootState) => state.auth.isAuthenticated);
  const [loading, setLoading] = useState(true);
  const [isSignUp, setIsSignUp] = useState(false);
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [name, setName] = useState<string>('');
  

  useEffect(() => {
    const storedAuth = localStorage.getItem('isAuthenticated') === 'true';
    if (isLoggedIn || storedAuth) {
      router.push('/'); // Redirect to home if logged in
    } else {
      setLoading(false); // No need for loading anymore if not authenticated
    }
  }, [isLoggedIn, router]);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_USER}/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Login failed: ${errorText}`);
      }

      const data = await response.json();
      if (data.ec === 0) {
        const token = data.dt;
        // Assuming decodeToken is defined elsewhere to decode JWT
        const user = decodeToken(token);

        if (user) {
          const userPayload = {
            name: user.name,
            userId: user.userId.toString(),
            exp: user.exp,
          };

          dispatch(login(userPayload));
          // Assuming saveToLocalStorage is defined elsewhere
          saveToLocalStorage(userPayload);
          router.push('/');
        } else {
          toast.error("Failed to decode user information.");
        }
      } else {
        toast.error(data.em);
      }
    } catch (error) {
      console.error("Error during login:", error);
      toast.error("An error occurred during login. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  const Register = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      const response = await fetch(`${process.env.NEXT_PUBLIC_USER}/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ username, password, email  ,name }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Registration failed: ${errorText}`);
      }

      const data = await response.json();
      if (data.ec === 0) {
        toast.success(data.em);
      } else {
        toast.error(data.em);
      }
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  // Show loading spinner while loading
  if (loading) {
    return (
      <div className="d-flex justify-content-center align-items-center" style={{ height: '100vh' }}>
        <Spinner animation="border" variant="primary" />
        <span className="ml-3">Loading...</span>
      </div>
    );
  }

  const togglePanel = () => {
    setIsSignUp(!isSignUp);
  };

  return (
    <div className="LoginPage">
      <div className={`container ${isSignUp ? 'right-panel-active' : ''}`} id="container">
        <div className="form-container sign-up-container">
          <form onSubmit={Register}>
            <h1>Create Account</h1>
            <div className="social-container">
              <a href="#" className="social"><i className="fab fa-facebook-f"></i></a>
              <a href="#" className="social"><i className="fab fa-google-plus-g"></i></a>
              <a href="#" className="social"><i className="fab fa-linkedin-in"></i></a>
            </div>
            <span>or use your email for registration</span>
            <input type="text" placeholder="Username" value={username} onChange={(event) => setUsername(event.target.value)} />
            <input type="text" placeholder="name" value={name} onChange={(event) => setName(event.target.value)} />
            <input type="password" placeholder="Password" value={password} onChange={(event) => setPassword(event.target.value)} />
            <input type="email" placeholder="Email" value={email} onChange={(event) => setEmail(event.target.value)} />
          
            <button type="submit">Sign Up</button>
          </form>
        </div>
        <div className="form-container sign-in-container">
          <form onSubmit={handleLogin}>
            <h1>Sign in</h1>
            <div className="social-container">
              <a href="#" className="social"><i className="fab fa-facebook-f"></i></a>
              <a href="#" className="social"><i className="fab fa-google-plus-g"></i></a>
              <a href="#" className="social"><i className="fab fa-linkedin-in"></i></a>
            </div>
            <span>or use your account</span>
            <input type="text" placeholder="Username" value={username} onChange={(event) => setUsername(event.target.value)} />
            <input type="password" placeholder="Password" value={password} onChange={(event) => setPassword(event.target.value)} />
            <a href="#">Forgot your password?</a>
            <button type="submit" disabled={loading}>{loading ? 'Logging in...' : 'Sign In'}</button>
          </form>
        </div>
        <div className="overlay-container">
          <div className="overlay">
            <div className="overlay-panel overlay-left">
              <h1>Welcome Back!</h1>
              <p>To keep connected with us please login with your personal info</p>
              <button className="ghost" onClick={togglePanel}>Sign In</button>
            </div>
            <div className="overlay-panel overlay-right">
              <h1>Hello, Friend!</h1>
              <p>Enter your personal details and start journey with us</p>
              <button className="ghost" onClick={togglePanel}>Sign Up</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;