import React, { createContext, useContext, useEffect, useState } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [isTokenExpired, setIsTokenExpired] = useState(false);
  const token = localStorage.getItem('token');

  useEffect(() => {
    const checkTokenExpiration = () => {
      if (token) {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const isExpired = payload.exp * 1000 < Date.now();

        if (isExpired) {
          setIsTokenExpired(true);
          localStorage.removeItem('token');
        } else {
          setIsTokenExpired(false);
        }
      }
    };

    checkTokenExpiration();
    const intervalId = setInterval(checkTokenExpiration, 5000);

    return () => clearInterval(intervalId);
  }, [token]);

  return (
    <AuthContext.Provider value={{ isTokenExpired }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
};