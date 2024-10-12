import { useNotification } from '../NotificationProvider/NotificationProvider';
import { registerUser } from '../../services/apiService';
import locales from '../../../public/locales/locales';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';
import i18n from '../../i18n';

const AdminRegister = ({ returnVisible }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const [role, setRole] = useState('User');
  const [email, setEmail] = useState('');
  const { t, i18n } = useTranslation();

  const handleChangeRole = (e) => {
    setRole(e.target.value);
  };

  const handleRegister = async (e) => {
    e.preventDefault();

    try {
      await registerUser(username, email, password, role);
      notifyInfo(`${t('auth.registration_success')}`);
    } catch (error) {
      notifyError(t(error.response.data.message)); 
    }
  };

  return (
    <>
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
            <select className="role-selector" value={role} onChange={handleChangeRole}>
              <option value="Admin">{t('roles.admin')}</option>
              <option value="User">{t('roles.user')}</option>
            </select>
            {username && password && email && role && (
            <button className='handle-request' onClick={() => handleRegister()}>
              {t('transaction.handle_operation')}
            </button> 
            )}
            <button onClick={returnVisible}>{t('transaction.go_back')}</button>
      </form>
    </>
  );
};

export default AdminRegister;