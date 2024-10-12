import { useNotification } from '../../NotificationProvider/NotificationProvider';
import { doTransfer } from '../../../services/apiService';
import locales from '../../../../public/locales/locales';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';
import i18n from '../../../i18n';

const Transfer = ({ fromAccount, returnVisible }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [toAccount, setToAccount] = useState('');
  const [amount, setAmount] = useState(0);
  const { t, i18n } = useTranslation();

  const handleTransfer = async (accountTo, amountTo) => {
    try {
      await doTransfer(fromAccount.data, accountTo, amountTo);
      notifyInfo(t('transaction.transfer_success'));
      setAmount('');
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };
  

  return (
    <>
      <div className='form-content'>
        <div className='input-form'>
          <h2>{t('transaction.transfer')}</h2>
          <div className='bank-buttons'>
            <button onClick={() => handleTransfer(toAccount, '50')}>$50</button>
            <button onClick={() => handleTransfer(toAccount, '10000')}>$10,000</button>
            <button onClick={() => handleTransfer(toAccount, '500')}>$500</button>
            <button onClick={() => handleTransfer(toAccount, '100000')}>$100,000</button>
            <button onClick={() => handleTransfer(toAccount, '2500')}>$2,500</button>
            <input 
              type="text" 
              placeholder={t('transaction.enter_amount')}
              value={amount} 
              onChange={(e) => setAmount(e.target.value)} 
              required 
            />
          </div>
            <input 
            type="text" 
            placeholder={t('transaction.enter_card')}
            value={toAccount} 
            onChange={(e) => setToAccount(e.target.value)} 
            required />
            
              {toAccount && (
                <button className='handle-request' onClick={() => handleTransfer(toAccount, amount)}>
                {t('transaction.handle_operation')}
              </button>
              )}
              <button onClick={returnVisible}>{t('transaction.go_back')}</button>
          </div>
      </div>
    </>
  );
};

export default Transfer;
