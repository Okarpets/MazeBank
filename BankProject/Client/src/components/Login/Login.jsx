import { useNotification } from '../NotificationProvider/NotificationProvider';
import { loginUser, getTokenRole } from '../../services/apiService';
import locales from '../../../public/locales/locales';
import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import Header from '../Header/Header';
import { Helmet } from 'react-helmet';
import i18n from '../../i18n';

const Login = () => {
  const { notifyError, notifyInfo } = useNotification();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const handleAuth = async () => {
    const userRole = await getTokenRole(); 
      if (userRole) {
        navigate(userRole === 'User' ? '/account' : '/admin');
      }
  }

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const response = await loginUser(username, password);
      localStorage.setItem('token', response.data.token);
      handleAuth();
      notifyInfo(`${'auth.login_success'}`);
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        handleAuth();
      } catch (error) {
        navigate('/login');
      }
    };

    fetchData();
  }, []);

  return (
    <>
      <div className='background-icons'>
        <Helmet>
          <title>MazeBank - {t('auth.login')}</title>
        </Helmet>
        <div className='main-content'>
          <Header />
          <div className='input-form'>
            <h2>{t('auth.login')}</h2>
            <input 
              type="text" 
              placeholder={t('auth.enter_username')}
              value={username} 
              onChange={(e) => setUsername(e.target.value)} 
              required 
            />
            <input 
              type="password" 
              placeholder={t('auth.enter_password')}
              value={password} 
              onChange={(e) => setPassword(e.target.value)} 
              required 
            />
            <button onClick={handleLogin}>{t('transaction.handle_operation')}</button>
            <p>{t('auth.no_account_prompt')} <a href="/register">{t('auth.register')}</a></p>
          </div>
        </div>
      </div>
    </>
  );
};

export default Login;