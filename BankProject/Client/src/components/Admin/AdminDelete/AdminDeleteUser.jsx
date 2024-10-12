import { useNotification } from '../../NotificationProvider/NotificationProvider';
import { deleteUserByName } from '../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';

const AdminDeleteAccount = ({ returnVisible }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [username, setUsername] = useState('');
  const { t, i18n } = useTranslation();
  
  const handleDelete = async () => {
    try {
      await deleteUserByName(username)
      notifyInfo(`${t('accounts.delete_success')}`)
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  return (
    <>
      <div className='form-content'>
        <div className='input-form'>
          <h2>{t('admin.delete_user')}</h2>
          <input 
              type="text" 
              placeholder={t('auth.enter_username')}
              value={username} 
              onChange={(e) => setUsername(e.target.value)} 
              required 
            />
          <div className='bank-buttons'>
            </div>
            {username && (
            <button className='handle-request' onClick={() => handleDelete()}>
              {t('transaction.handle_operation')}
            </button>
          )}
          <button onClick={returnVisible}>{t('transaction.go_back')}</button>
        </div>
      </div>
    </>
  );
};

export default AdminDeleteAccount;
