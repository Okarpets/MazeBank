import { useNotification } from '../../NotificationProvider/NotificationProvider';
import { doDepositByName } from '../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';

const AdminDeposite = ({ returnVisible }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [account, setAccount] = useState('');
  const [amount, setAmount] = useState('');
  const { t, i18n } = useTranslation();
  
  
  const handleDeposite = async () => {
    try {
      await doDepositByName(account, amount);
      notifyInfo(`${t('transaction.deposit_success')} +${amount}`)
      setAmount('');
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  return (
    <>
      <div className='form-content'>
        <div className='input-form'>
          <h2>{t('transaction.deposit')}</h2>
          <input 
              type="text" 
              placeholder={t('transaction.enter_card')} 
              value={account} 
              onChange={(e) => setAccount(e.target.value)} 
              required 
            />
            <input 
              type="text" 
              placeholder={t('transaction.enter_amount')} 
              value={amount} 
              onChange={(e) => setAmount(e.target.value)} 
              required 
            />
          <div className='bank-buttons'>
            </div>
            {amount && account && (
            <button className='handle-request' onClick={() => handleDeposite()}>
              {t('transaction.handle_operation')}
            </button>
          )}
          <button onClick={returnVisible}>{t('transaction.go_back')}</button>
        </div>
      </div>
    </>
  );
};

export default AdminDeposite;
