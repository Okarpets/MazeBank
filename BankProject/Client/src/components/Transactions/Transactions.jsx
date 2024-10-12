import { useNotification } from '../NotificationProvider/NotificationProvider';
import OperationDetail from './TransactionForms/OperationDetail';
import { useParams, useNavigate } from 'react-router-dom';
import locales from '../../../public/locales/locales';
import Transfer from './TransactionForms/Transfer';
import Deposite from './TransactionForms/Deposite';
import Withdraw from './TransactionForms/Withdraw';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import Header from '../Header/Header';
import { Helmet } from 'react-helmet';
import i18n from '../../i18n';
import { 
  getTransactionsForAccount,
  deleteAccountByNumber, 
  getAccountNumberById, 
  getUsernameFromApi,
} from '../../services/apiService';

const Transactions = () => {
  const [accountNumber, setAccountNumber] = useState('');
  const [transactions, setTransactions] = useState([]);
  const [component, setComponent] = useState(null);
  const [username, setUsername] = useState('');
  const [visible, setVisible] = useState(true);
  const { notifyError, notifyInfo } = useNotification();
  const { t, i18n } = useTranslation();
  const { accountId } = useParams();
  const navigate = useNavigate();
  
  const [headContent, setHeadContent] = useState(t('transaction.transaction_overview'));

  const fetchUsername = async () => {
    const response = await getUsernameFromApi();
    setUsername(response.data);
  };

  const handleDelete = async () => {
    try {
      const response = await deleteAccountByNumber(accountNumber.data);
      if (response.data.status) {
        notifyInfo(t('accounts.delete_success'));
        navigate('/account');
      }
    } catch (error) {
      notifyError(t(error.response.data));
    }
  };

  useEffect(() => {
    const fetchTransactions = async () => {
      try {
        const accountNumber = await getAccountNumberById(accountId);
        setAccountNumber(accountNumber);
        const request = await getTransactionsForAccount(accountId);
        setTransactions(request.data);
      } catch (error) {
        if (error.response?.status === 403) {
          navigate('/account');
        }
      }
    };

    fetchUsername();
    fetchTransactions();
  }, [accountId, navigate, notifyError]);

  const handleOperationDetails = (id) => {
    setComponent(() => <OperationDetail returnVisible={() => { setVisible(true); setComponent(null); }} operationId={id}/>)
    setVisible(false)
  };

  const renderTransactions = () => {
    return transactions.map(({ id, description }) => (
      <li key={id}>
        <button className='transacts' onClick={() => handleOperationDetails(id)}>
            <p className='operation-details'>
                {description}
            </p>
        </button>
      </li>
    ));
  };

  const handleButtonClick = (Component, returnVisibleFunc, headContent, accountId) => {
    setComponent(() => 
      <Component 
        returnVisible={returnVisibleFunc} 
        account={accountId}
        fromAccount={accountNumber}
      />
    );
    setHeadContent(headContent);
    setVisible(false);
  };
  
  const returnVisibleFunc = () => {
    setVisible(true);
    setComponent(null);
    setHeadContent(t('transaction.transaction_overview'));
  };

  const handleExit = () => {

  }
  
  return (
    <>
      <div className='background-icons'>
        <Helmet>
          <title>MazeBank - {t('transaction.transaction_history')}</title>
        </Helmet>
        <div className='main-content'>
          <Header isVisible={true} headContent={headContent} fetchBalance={accountId}/>
          <div className='transactions-form'>
            {component ? component : (
              <ul>{renderTransactions()}</ul>
            )}
          </div>
          <div>
            {visible && (
              <>
                <div className='input-form'> 
                  <button onClick={() => handleButtonClick(Transfer, returnVisibleFunc, t('transaction.transfer_prompt'), accountNumber)}>
                  {t('transaction.transfer')}
                  </button>
                  <button onClick={() => handleButtonClick(Deposite, returnVisibleFunc, t('transaction.deposit_prompt'), accountId)}>
                  {t('transaction.deposit')}
                  </button>
                  <button onClick={() => handleButtonClick(Withdraw, returnVisibleFunc, t('transaction.withdraw_prompt'), accountId)}>
                  {t('transaction.withdraw')}
                  </button>
                  <button onClick={() => navigate('/account')}>{t('transaction.go_back')}</button>
                  <button onClick={handleDelete}>{t('accounts.delete_account')}</button>
                </div>
              </>
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default Transactions;
