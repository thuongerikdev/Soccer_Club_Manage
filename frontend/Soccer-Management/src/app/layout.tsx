'use client';
import { Inter } from 'next/font/google';
import 'bootstrap/dist/css/bootstrap.min.css';
import AppFooter from '@/components/footer/app.footer';
import Container from 'react-bootstrap/Container';
import 'react-toastify/dist/ReactToastify.css';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import { ToastContainer } from 'react-toastify';
import AppHeader from '@/components/header/app.header';
import { Provider } from 'react-redux';
import store from '../lib/store';

const inter = Inter({ subsets: ['latin'] });

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <Provider store={store}>
      <html lang="en">
        <head>
          <title>My Website</title>
          <link rel="icon" href="/favicon.ico" type="image/x-icon" />
        </head>
        <body className={inter.className} style={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
          <AppHeader />
          <Container>
            {children}
          </Container>
          <AppFooter />
          <ToastContainer
            position="top-center"
            autoClose={5000}
            hideProgressBar={false}
            newestOnTop={false}
            closeOnClick
            rtl={false}
            pauseOnFocusLoss
            draggable
            pauseOnHover
            theme="light"
          />
        </body>
      </html>
    </Provider>
  );
}