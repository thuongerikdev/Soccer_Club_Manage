// store.ts

import { configureStore } from '@reduxjs/toolkit';
import authReducer from './features/loginSlice' ;
import { loadFromLocalStorage } from "@/lib/ReloginAction";
const store = configureStore({
    reducer: {
        auth: authReducer,
    },
    preloadedState: {
        // auth: loadFromLocalStorage(), // Sử dụng hàm đã định nghĩa để khởi tạo state
    },
});
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;


// const loadUserFromLocalStorage = (): { isAuthenticated: boolean; user: User | null } => {
//   const user = localStorage.getItem('user');
//   if (user) {
//       try {
//           return {
//               isAuthenticated: true,
//               user: JSON.parse(user),
//           };
//       } catch (error) {
//           console.error("Error parsing user from localStorage:", error);
//           return {
//               isAuthenticated: false,
//               user: null,
//           };
//       }
//   }
//   return {
//       isAuthenticated: false,
//       user: null,
//   };
// };
// const { isAuthenticated, user } = loadUserFromLocalStorage();

// const store = configureStore({
//     reducer: {
//         auth: authReducer,
//     },
//     preloadedState: {
//         auth: {
//             isAuthenticated,
//             user,
//         },
//     },
// });
// // const store = configureStore({
// //   reducer: {
// //     auth: authReducer,
// //   },
// // });
// export type RootState = ReturnType<typeof store.getState>;
// export type AppDispatch = typeof store.dispatch;

// export default store;
