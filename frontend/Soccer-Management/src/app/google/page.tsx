// LoginPage.tsx
'use client'; // Đảm bảo đây là Client Component

import React from 'react';
import { GoogleLogin, CredentialResponse } from '@react-oauth/google';

const LoginPage: React.FC = () => {
    const handleGoogleLoginSuccess = async (credentialResponse: CredentialResponse) => {
        const googleToken = credentialResponse.credential;

        try {
            const response = await fetch('http://localhost:3001/api/users/login/google', { // Sửa đường dẫn API
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ googleToken }),
            });

            if (!response.ok) {
                throw new Error('Network response was not ok');
            }

            const data = await response.json();
            console.log("Login Successful:", data);
            // Bạn có thể điều hướng đến trang chính hoặc thực hiện hành động khác sau khi đăng nhập thành công
        } catch (error) {
            console.error("Login Failed:", error);
        }
    };

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <GoogleLogin
                onSuccess={handleGoogleLoginSuccess} // Đảm bảo hàm này được định nghĩa trong Client Component
                onError={() => console.log('Login Failed')} // Hàm xử lý khi đăng nhập thất bại
            />
        </div>
    );
};

export default LoginPage;