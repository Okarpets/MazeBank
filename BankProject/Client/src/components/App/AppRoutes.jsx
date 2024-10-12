import ProtectedRoute from '../ProtectedRoute/ProtectedRoute';
import { Routes, Route, Navigate } from 'react-router-dom';
import Transactions from '../Transactions/Transactions';
import { useAuth } from '../AuthContext/AuthContext';
import Register from '../Register/Register';
import Account from '../Account/Account';
import Login from '../Login/Login';
import Admin from '../Admin/Admin';
import React from 'react';

const AppRoutes = () => {
  const { isTokenExpired } = useAuth();

  if (isTokenExpired) {
    return <Navigate to="/login" />;
  }

  return (
    <Routes>
      <Route path="/" element={<Navigate to="/login" />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route 
      path="/account"
      element={
        <ProtectedRoute
          allowedRoles={['Admin', 'User']}
          element={<Account />}
        />
      } 
    />
      <Route 
        path="/transactions/:accountId" 
        element={
          <ProtectedRoute
            allowedRoles={['Admin', 'User']}
            element={<Transactions />}
          />
        } 
      />
      <Route
        path="/admin"
        element={
          <ProtectedRoute
            allowedRoles={['Admin']}
            element={<Admin />}
          />
        }
      />
    </Routes>
  );
};

export default AppRoutes;
