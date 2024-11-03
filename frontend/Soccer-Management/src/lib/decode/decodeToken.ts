// lib/decodeToken.ts
import { jwtDecode } from 'jwt-decode';

// Định nghĩa kiểu cho payload của token
interface TokenPayload {
    name: string;
    userId: number; // hoặc string, tùy thuộc vào cách bạn định nghĩa userId
    exp: number; // Thời gian hết hạn
}

// Hàm giải mã token
export const decodeToken = (token: string): TokenPayload | null => {
    try {
        return jwtDecode<TokenPayload>(token);
    } catch (error) {
        console.error('Failed to decode token:', error);
        return null; // Trả về null nếu có lỗi
    }
};