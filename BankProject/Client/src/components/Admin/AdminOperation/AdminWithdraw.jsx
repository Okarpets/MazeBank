import { useNotification } from '../../NotificationProvider/NotificationProvider';
import { doWithdrawByName } from '../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';

const AdminWithdraw = ({ returnVisible }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [account, setAccount] = useState('');
  const [amount, setAmount] = useState('');
  const { t, i18n } = useTranslation();

  const handleWithdraw = async (amountToWithdraw) => {
    try {
      await doWithdrawByName(account, amountToWithdraw);
      notifyInfo(`${t('transaction.withdraw_success')} -${amountToWithdraw}`)
      setAmount(''); 
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  return (
    <>
      <div className='form-content'>
        <div className='input-form'>
          <h2>{t('transaction.withdraw')}</h2>
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
          {amount && (
            <button className='handle-request' onClick={() => handleWithdraw(amount)}>
              {t('transaction.handle_operation')}
            </button>
          )}
          <button onClick={returnVisible}>{t('transaction.go_back')}</button>
        </div>
      </div>
    </>
  );
};

export default AdminWithdraw;
