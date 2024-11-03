// components/withAuth.tsx
import { useSelector, useDispatch } from 'react-redux';
import { checkAuth } from '../../lib/features/loginSlice';
import { RootState } from '../../lib/store';
import { useRouter } from 'next/navigation'; // For app directory
import { useEffect, useState } from 'react';

const withAuth = <P extends object>(WrappedComponent: React.ComponentType<P>) => {
  const AuthenticatedComponent = (props: P) => {
    const dispatch = useDispatch();
    const router = useRouter();
    const isAuthenticated = useSelector((state: RootState) => state.auth.isAuthenticated);
    const [loading, setLoading] = useState(true); // Trạng thái loading

    useEffect(() => {
      // Kiểm tra xem người dùng đã xác thực chưa
      if (!isAuthenticated) {
        dispatch(checkAuth()); // Gọi action xác thực
      } else {
        setLoading(false); // Nếu đã xác thực, không còn loading
      }
    }, [isAuthenticated, dispatch]);

    useEffect(() => {
      if (!isAuthenticated && typeof window !== 'undefined') {
        router.push('/login'); // Chuyển hướng về trang login
      } else {
        setLoading(false); // Nếu đã xác thực, không còn loading
      }
    }, [isAuthenticated, router]);

    // Hiển thị spinner loading trong thời gian chờ xác thực
    if (loading) {
      return <div>Loading...</div>; // Hoặc bạn có thể sử dụng một spinner
    }

    return <WrappedComponent {...props} />;
  };

  return AuthenticatedComponent;
};

export default withAuth;