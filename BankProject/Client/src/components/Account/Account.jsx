import { useNotification } from '../NotificationProvider/NotificationProvider';
import ChangeUsername from './AccountForms/ChangeUsername/ChangeUsername';
import ResetPassword from './AccountForms/ResetPassword/ResetPassword';
import React, { useState, useEffect } from 'react';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import Header from '../Header/Header';
import { Helmet } from 'react-helmet';
import './Account.css'
import {
  getUsernameFromApi,
  deleteUserById,
  createAccount,
  getAccounts,
  getTokenId
} from '../../services/apiService';

const Account = () => {
  const { notifyError, notifyInfo } = useNotification();
  const [component, setComponent] = useState(null);
  const [username, setUsername] = useState('');
  const [accounts, setAccounts] = useState([]);
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const userIdFromToken = getTokenId();

  const fetchAccounts = async () => {
    try {
      const response = await getAccounts(userIdFromToken);
      setAccounts(response.data);
    } catch (error) {
      handleFetchError(error);
    }
  };

  const fetchUsername = async () => {
    const response = await getUsernameFromApi();
    setUsername(response.data);
  };

  const handleCreateAccount = async () => {
    try {
      await createAccount(userIdFromToken);
      fetchAccounts();
      notifyInfo(`${t('accounts.account_creation_success')}`);
    } catch (error) {
      notifyError(`${t('accounts.account_creation_failure')} ${error.response.data.message}`);
    }
  };

  const handleFetchError = (error) => {
    if (error.response && error.response.status === 403) {
      navigate('/account');
    }
  };

  const handleViewTransactions = (accountId) => {
    navigate(`/transactions/${accountId}`);
  };

  const handleDelete = async () => {
    try {
      const response = await deleteUserById(userIdFromToken);
      if (response.data.status) {
        localStorage.removeItem('token');
        notifyInfo(t('accounts.delete_success'));
        navigate('/login');
      }
    } catch (error) {
      notifyError(t(error.response.data.message));
    }
  };

  const handleExit = () => {
    localStorage.removeItem('token');
    notifyInfo(`${t('accounts.exit_success')}`)
    navigate('/login');
  };

  const handlePasswordReset = () => {
    setComponent(<ResetPassword cancelForm={() => setComponent(null)} />);
  };

  const handleChangeUsername = () => {
    setComponent(<ChangeUsername isChanged={fetchUsername} cancelForm={() => setComponent(null)} />);
  };

  useEffect(() => {
    fetchUsername();
    fetchAccounts();
  }, []);

  return (
    <div className='background-icons'>
      <Helmet>
        <title>MazeBank - {t('accounts.title')}</title>
      </Helmet>
      <div className='main-content'>
        <Header messageContent={username}/>
        <h2>{t('accounts.user_accounts')} {accounts.length}</h2>
        <ul>
          {accounts.map((account) => (
            <li key={account.id}>
              <button className='transacts' onClick={() => handleViewTransactions(account.id, account.accountNumber)}>
                <p className='transacts-account'>
                  {account.accountNumber}
                </p>
                <p className='transacts-balance'>
                  {account.balance}
                </p>
              </button>
            </li>
          ))}
        </ul>
        <div className='user-menu'>
          {component}
          <div className='buttons-form'>
            <button onClick={handlePasswordReset}>{t('accounts.reset_password')}</button>
            <button onClick={handleChangeUsername}>{t('accounts.change_username')}</button>
            <button onClick={handleCreateAccount}>{t('accounts.create_new_account')}</button>
            <button onClick={handleDelete}>{t('accounts.delete_account')}</button>
            <button onClick={handleExit}>{t('accounts.exit_account')}</button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Account;
