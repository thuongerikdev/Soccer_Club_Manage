'use client';
import Image from "next/image";
import './page.scss';
import React, { useState } from "react";
import { Button, Form } from 'react-bootstrap';
import { useRouter } from 'next/navigation';
import useSWR, { mutate } from 'swr';
import { useSelector, useDispatch } from 'react-redux'
import { login, logout } from "@/lib/features/loginSlice";
import { toast } from "react-toastify";

const LoginPage = () => {
  const router = useRouter();
  const dispatch = useDispatch();
  const [isSignUp, setIsSignUp] = useState(false);
  const [loading, setLoading] = useState(false);
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [email, setEmail] = useState<string>('');

  const togglePanel = () => {
    setIsSignUp(!isSignUp);
  };

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
        const errorText = await response.text(); // Get the response as text
        throw new Error(`Login failed: ${errorText}`);
      }
      const data = await response.json();
      if (data.ec === 0) {
        dispatch(login(data.dt))
        router.push('/');
      }
      console.log(data)


    } catch (error) {
      console.error(error);
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
        body: JSON.stringify({ username, password, email }),
      });

      if (!response.ok) {
        const errorText = await response.text(); // Get the response as text
        throw new Error(`Login failed: ${errorText}`);
      }
      const data = await response.json();
      if (data.ec === 0) {
        toast.success(data.em)
      }
      if(data.ec !=0) {
        toast.error(data.em)
      }


    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  }

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