import { getTokenRole } from '../../services/apiService';
import { Navigate, useNavigate } from 'react-router-dom';
import React, { useEffect } from 'react';

const ProtectedRoute = ({ element, allowedRoles }) => {
  const userRole = getTokenRole();
  const navigate = useNavigate();

  useEffect(() => {
    const checkToken = () => {
      const token = localStorage.getItem('token');
      if (!token) {
        navigate('/login');
      }
    };

    checkToken();
    
    const intervalId = setInterval(checkToken, 5000);

    return () => clearInterval(intervalId);
  }, [navigate]);

  if (allowedRoles && !allowedRoles.includes(userRole)) {
    return <Navigate to="/" />;
  }

  return element;
};

export default ProtectedRoute;
