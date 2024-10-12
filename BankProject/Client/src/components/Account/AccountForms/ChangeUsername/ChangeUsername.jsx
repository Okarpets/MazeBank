import { useNotification } from '../../../NotificationProvider/NotificationProvider';
import { usernameReset } from '../../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';


const ChangeUsername = ({ cancelForm, isChanged }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [password, setPassword] = useState('');
  const [username, setUsername] = useState('');
  const { t, i18n } = useTranslation();

  const handleUsername = async (e) => {
    e.preventDefault();

    try {
      await usernameReset(username, password);
      cancelForm();
      isChanged()
      notifyInfo(`${t('accounts.reset_success')}`);
    } catch (error) {
      notifyError(`${t('accounts.reset_failure')}`);
    }
  };

  return (
    <>
      <div className='form-content'>
          <div className='input-form'>
            <h2>{t('accounts.change_username')}</h2>
            <input 
            type="text" 
            placeholder={t('accounts.new_username')} 
            value={username} 
            onChange={(e) => setUsername(e.target.value)} 
            required />
            <input 
            type="password" 
            placeholder={t('auth.enter_password')}
            value={password} 
            onChange={(e) => setPassword(e.target.value)} 
            required />

          <button onClick={handleUsername}>{t('transaction.handle_operation')}</button>
          <button onClick={cancelForm}>{t('accounts.cancel')}</button>
          </div>
      </div>
    </>
  );
};

export default ChangeUsername;
