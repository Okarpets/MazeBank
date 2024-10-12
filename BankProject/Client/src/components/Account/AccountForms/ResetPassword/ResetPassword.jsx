import { useNotification } from '../../../NotificationProvider/NotificationProvider';
import { passwordReset } from '../../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';

const ResetPassword = ({ cancelForm }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [oldPassword, setOldPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const { t, i18n } = useTranslation();

  const handleReset = async (e) => {
    e.preventDefault();

    try {
      await passwordReset(oldPassword, newPassword);
      cancelForm();
      notifyInfo(`${t('accounts.reset_success')}`);
    } catch (error) {
      notifyError(`${t('accounts.reset_failure')}`);
    }
  };

  return (
    <>
      <div className='form-content'>
          <div className='input-form'>
            <h2>{t('accounts.reset_password')}</h2>
            <input 
            type="password" 
            placeholder={t('accounts.old_password')}
            value={oldPassword} 
            onChange={(e) => setOldPassword(e.target.value)} 
            required />

            <input 
            type="password" 
            placeholder={t('accounts.new_password')}
            value={newPassword} 
            onChange={(e) => setNewPassword(e.target.value)} 
            required />
            
            <button onClick={handleReset}>{t('transaction.handle_operation')}</button>
            <button onClick={cancelForm}>{t('accounts.cancel')}</button>
          </div>
      </div>
    </>
  );
};

export default ResetPassword;
