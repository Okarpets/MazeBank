import React, { createContext, useState, useContext } from 'react';
import './NotificationProvider.css';
import ReactDOM from 'react-dom';

const NotificationContext = createContext();

export const useNotification = () => {
  return useContext(NotificationContext);
};

export const NotificationProvider = ({ children }) => {
  const [error, setError] = useState('');
  const [info, setInfo] = useState('');

  const notifyError = (message) => {
    setError(message);
    setTimeout(() => setError(''), 5000);
  };

  const notifyInfo = (message) => {
    setInfo(message);
    setTimeout(() => setInfo(''), 5000);
  };

  return (
    <NotificationContext.Provider value={{ notifyError, notifyInfo }}>
      {children}
      {error && ReactDOM.createPortal(
        <div className="error-message">{error}</div>,
        document.getElementById('error-content')
      )}
      {info && ReactDOM.createPortal(
        <div className="info-message">{info}</div>,
        document.getElementById('info-content')
      )}
    </NotificationContext.Provider>
  );
};
