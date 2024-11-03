// middleware.ts
// import { NextResponse } from 'next/server';
// import type { NextRequest } from 'next/server';

// export function middleware(req: NextRequest) {
//   const cookie = req.cookies.get('isAuthenticated');
//   const isLoggedIn = cookie?.value === 'true'; // Safely check the cookie value

//   if (req.nextUrl.pathname === '/login' && isLoggedIn) {
//     return NextResponse.redirect(new URL('/', req.url)); // Redirect to home if logged in
//   }

//   return NextResponse.next();
// }

// export const config = {
//   matcher: ['/login'], // Apply middleware to the login page
// };