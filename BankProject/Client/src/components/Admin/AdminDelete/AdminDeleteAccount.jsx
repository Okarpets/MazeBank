import { useNotification } from '../../NotificationProvider/NotificationProvider';
import { deleteAccountByNumber } from '../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';

const AdminDeleteAccount = ({ returnVisible }) => {
  const [accountNumber, setAccountNumber] = useState('');
  const { notifyError, notifyInfo } = useNotification();
  const { t, i18n } = useTranslation();

  const handleDelete = async () => {
    try {
      await deleteAccountByNumber(accountNumber)
      notifyInfo(`${t('accounts.delete_success')}`)
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  return (
    <>
      <div className='form-content'>
        <div className='input-form'>
          <h2>{t('accounts.delete_account')}</h2>
          <input 
              type="text" 
              placeholder={t('transaction.enter_card')}
              value={accountNumber} 
              onChange={(e) => setAccountNumber(e.target.value)} 
              required 
            />
          <div className='bank-buttons'>
            </div>
            {accountNumber && (
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