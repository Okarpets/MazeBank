import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import { Helmet } from 'react-helmet';
import Header from '../Header/Header';
import OperationDetail from './TransactionForms/OperationDetail';
import { useNotification } from '../NotificationProvider/NotificationProvider';
import Transfer from './TransactionForms/Transfer';
import Deposite from './TransactionForms/Deposite';
import Withdraw from './TransactionForms/Withdraw';
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
  const [filter, setFilter] = useState(localStorage.getItem('transactionFilter') || 0);
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
        const request = await getTransactionsForAccount(accountId, filter);
        console.log(request.data);
        setTransactions(request.data);
      } catch (error) {
        if (error.response?.status === 403) {
          navigate('/account');
        }
      }
    };

    fetchUsername();
    fetchTransactions();
  }, [accountId, filter, navigate, notifyError]);

  useEffect(() => {
    setHeadContent(t('transaction.transaction_overview'));
  }, [i18n.language, t]);

  const handleOperationDetails = (id) => {
    setComponent(() => (
      <OperationDetail
        returnVisible={() => {
          setVisible(true);
          setComponent(null);
        }}
        operationId={id}
      />
    ));
    setVisible(false);
  };

  const renderTransactions = () => {
    return transactions.map(({ id, transactionType, amount, transactionDate, toAccountNumber, fromAccountNumber }) => (
      <li key={id}>
        <button className="transacts" onClick={() => handleOperationDetails(id)}>
        <p className="operation-details">
          {t(`transaction_data.${transactionType}`)} | {t('transaction_data.amount')} {amount} 
          {transactionType === 3 && ` | ${t('transaction_data.from')} ${fromAccountNumber} ${t('transaction_data.to')} ${toAccountNumber}`} |
          {t('transaction_data.date')} {transactionDate}
        </p>
        </button>
      </li>
    ));
  };

  const handleButtonClick = (Component, returnVisibleFunc, headContent, accountId) => {
    setComponent(() => (
      <Component returnVisible={returnVisibleFunc} account={accountId} fromAccount={accountNumber} />
    ));
    setHeadContent(headContent);
    setVisible(false);
  };

  const returnVisibleFunc = () => {
    setVisible(true);
    setComponent(null);
    setHeadContent(t('transaction.transaction_overview'));
  };

  const handleFilterChange = (event) => {
    const newFilter = event.target.value;
    setFilter(newFilter);
    localStorage.setItem('transactionFilter', newFilter);
  };

  return (
    <>
      <div className="background-icons">
        <Helmet>
          <title>MazeBank - {t('transaction.transaction_history')}</title>
        </Helmet>
        <div className="main-content">
          <Header isVisible={true} headContent={headContent} fetchBalance={accountId} />
          <div className="transactions-form">
            {component ? component : <ul>{renderTransactions()}</ul>}
          </div>
          <div>
            {visible && (
              <div className="input-form">
                <select className="selector" value={filter} onChange={handleFilterChange}>
                  <option value="0">{t('filter.all')}</option>
                  <option value="1">{t('filter.deposit')}</option>
                  <option value="2">{t('filter.withdraw')}</option>
                  <option value="3">{t('filter.transfer')}</option>
                </select>
                <button
                  onClick={() =>
                    handleButtonClick(Transfer, returnVisibleFunc, t('transaction.transfer_prompt'), accountNumber)
                  }
                >
                  {t('transaction.transfer')}
                </button>
                <button
                  onClick={() =>
                    handleButtonClick(Deposite, returnVisibleFunc, t('transaction.deposit_prompt'), accountId)
                  }
                >
                  {t('transaction.deposit')}
                </button>
                <button
                  onClick={() =>
                    handleButtonClick(Withdraw, returnVisibleFunc, t('transaction.withdraw_prompt'), accountId)
                  }
                >
                  {t('transaction.withdraw')}
                </button>
                <button onClick={() => navigate('/account')}>{t('transaction.go_back')}</button>
                <button onClick={handleDelete}>{t('accounts.delete_account')}</button>
              </div>
            )}
          </div>
        </div>
      </div>
    </>
  );
};

export default Transactions;
