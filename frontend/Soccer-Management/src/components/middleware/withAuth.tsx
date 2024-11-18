// components/withAuth.tsx
import { useSelector } from 'react-redux';
import { useRouter, usePathname } from 'next/navigation'; 
import { useEffect, useState } from 'react';
import { RootState } from '../../lib/store';

const withAuth = <P extends { userRole?: string | null }>(WrappedComponent: React.ComponentType<P>) => {
  const AuthenticatedComponent = (props: Omit<P, 'userRole' | 'loading'>) => { 
    const router = useRouter();
    const pathname = usePathname(); 
    const isAuthenticated = useSelector((state: RootState) => state.auth.isAuthenticated);
    const userId = useSelector((state: RootState) => state.auth.user?.userId);
    const [loading, setLoading] = useState(true);
    const [userRole, setUserRole] = useState<string | null>(null);

    useEffect(() => {
      const fetchUserRole = async () => {
        try {
          const roleResponse = await fetch(`http://localhost:3001/api/authuserrole/getRolebyUser/${userId}`);
          if (!roleResponse.ok) {
            throw new Error('Failed to fetch user role');
          }
          const roleData = await roleResponse.json();

          const nameResponse = await fetch(`http://localhost:3001/api/authrole/getrole/${roleData.dt.roleId}`);
          if (!nameResponse.ok) {
            throw new Error('Failed to fetch role name');
          }
          const nameData = await nameResponse.json();
          setUserRole(nameData.dt.roleName);
        } catch (error) {
          console.error('Error fetching user role:', error);
        } finally {
          setLoading(false);
        }
      };

      if (isAuthenticated) {
        fetchUserRole();
      } else {
        setLoading(false);
        router.push('/login');
      }
    }, [isAuthenticated, userId, router]);

    // Role-based access control
    useEffect(() => {
      if (!loading) {
        if (userRole === 'clubmanager' && pathname === '/users') {
          router.push('/');
        }
      }
    }, [loading, userRole, pathname, router]);

    return <WrappedComponent {...(props as P)} userRole={userRole} loading={loading} />;
  };

  return AuthenticatedComponent;
};

export default withAuth;