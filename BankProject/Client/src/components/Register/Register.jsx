import { useNotification } from '../NotificationProvider/NotificationProvider';
import { registerUser, loginUser } from '../../services/apiService';
import locales from '../../../public/locales/locales';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';
import Header from '../Header/Header';
import { Helmet } from 'react-helmet';
import i18n from '../../i18n';

const Register = () => {
  const { notifyError, notifyInfo } = useNotification(); 
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();
  
  const handleRegister = async (e) => {
    e.preventDefault();
    notifyError('')

    try {
      await registerUser(username, email, password, 'User');

      await handleLogin(username, password);
      notifyInfo(`${t('auth.registration_success')}`);
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  const handleLogin = async (username, password) => {
    try {
      const loginResponse = await loginUser(username, password);
      localStorage.setItem('token', loginResponse.data.token);
      navigate('/account');
      notifyInfo(`${t('auth.login_success')}`);
    } catch (error) {
      notifyError(`${t('auth.login_failure')}`);
    }
  };

  return (
    <>
      <Helmet>
        <title>MazeBank - {t('auth.register')}</title>
      </Helmet>
      <div className='background-icons'>
        <div className='main-content'>
          <Header />
          <form className='input-form' onSubmit={handleRegister}>
            <input
              type="text"
              name="username"
              placeholder={t('auth.enter_username')}
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
            <input
              type="email"
              name="email"
              placeholder={t('auth.enter_email')}
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <input
              type="password"
              name="password"
              placeholder={t('auth.enter_password')}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
            <button type="submit">{t('transaction.handle_operation')}</button>
          </form>
          <p>
          {t('auth.already_have_account')} <a href="/login">{t('auth.login')}</a>
          </p>
        </div>
      </div>
    </>
  );
};

export default Register;
