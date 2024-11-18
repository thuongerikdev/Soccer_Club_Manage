// features/auth/authSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';


interface AuthState {
    isAuthenticated: boolean;
    user: User | null; // user can be User type or null
    
}

const initialState: AuthState = {
    isAuthenticated: false,
    user: null,
};

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        login(state, action: PayloadAction<User>) {
            state.isAuthenticated = true;
            state.user = action.payload; // action.payload should be User
            localStorage.setItem('isAuthenticated', 'true');
        },
        logout(state) {
            state.isAuthenticated = false;
            state.user = null;
            localStorage.removeItem('user');
            localStorage.removeItem('isAuthenticated');
        },
        setUser(state, action: PayloadAction<{ user: User; isAuthenticated: boolean }>) {
          state.user = action.payload.user; // Lưu thông tin người dùng
          state.isAuthenticated = action.payload.isAuthenticated; // Cập nhật trạng thái xác thực
         
      },
      checkAuth(state) {
        const authStatus = localStorage.getItem('isAuthenticated');
        state.isAuthenticated = authStatus === 'true';
      },
    },
});

export const { login, logout, setUser , checkAuth} = authSlice.actions;
export default authSlice.reducer;