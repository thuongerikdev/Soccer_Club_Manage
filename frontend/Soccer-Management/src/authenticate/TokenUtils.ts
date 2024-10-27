// utils/tokenUtils.ts
import { jwtDecode ,JwtPayload} from 'jwt-decode';

/**
 * Interface for the custom JWT payload.
 */
interface CustomJwtPayload extends JwtPayload {
    userId: string;
    username: string;
    role: string;
}

/**
 * Decode a JWT token and return the payload.
 * @param {string | null} token - The JWT token to decode.
 * @returns {CustomJwtPayload | null} - The decoded payload or null if invalid.
 */
export function decodeToken(token: string | null): CustomJwtPayload | null {
    if (!token) {
        return null;
    }

    try {
        const decoded = jwtDecode<CustomJwtPayload>(token);
        return decoded;
    } catch (error) {
        console.error('Invalid token', error);
        return null;
    }
}