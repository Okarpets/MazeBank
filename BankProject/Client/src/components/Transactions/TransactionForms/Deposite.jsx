import { useNotification } from '../../NotificationProvider/NotificationProvider';
import locales from '../../../../public/locales/locales';
import { doDeposit } from '../../../services/apiService';
import { useTranslation } from 'react-i18next';
import React, { useState } from 'react';
import i18n from '../../../i18n';

const Deposite = ({ account, returnVisible }) => {
  const { notifyError, notifyInfo } = useNotification();
  const [amount, setAmount] = useState('');
  const { t, i18n } = useTranslation();

  const handleDeposite = async (amountToDeposit) => {
    try {
      await doDeposit(account, amountToDeposit);
      notifyInfo(`${t('transaction.deposit_success')} +${amountToDeposit}`)
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
          <div className='bank-buttons'>
            <button onClick={() => handleDeposite('50')}>$50</button>
            <button onClick={() => handleDeposite('10000')}>$10,000</button>
            <button onClick={() => handleDeposite('500')}>$500</button>
            <button onClick={() => handleDeposite('100000')}>$100,000</button>
            <button onClick={() => handleDeposite('2500')}>$2,500</button>
            <input 
              type="text" 
              placeholder={t('transaction.enter_amount')}
              value={amount} 
              onChange={(e) => setAmount(e.target.value)} 
              required 
            />
            </div>
            {amount && (
            <button className='handle-request' onClick={() => handleDeposite(amount)}>
              {t('transaction.handle_operation')}
            </button>
          )}
          <button onClick={returnVisible}>{t('transaction.go_back')}</button>
        </div>
      </div>
    </>
  );
};

export default Deposite;
