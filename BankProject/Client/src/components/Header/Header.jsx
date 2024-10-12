import { getUsernameFromApi, getBalance } from '../../services/apiService';
import ThemeSwitcher from '../ThemeProvider/ThemeSwither/ThemeSwitcher';
import LangSelector from '../LangSelector/LangSelector';
import locales from '../../../public/locales/locales';
import { useTranslation } from 'react-i18next';
import { useState, useEffect } from 'react';
import Logo from '../Logo/Logo';
import i18n from '../../i18n';
import React from 'react';
import './Header.css';

const Header = ({ messageContent, isVisible, headContent, fetchBalance }) => {
  const [accountNumber, setAccountNumber] = useState('');
  const [visibleInfo, setVisibleInfo] = useState(false);
  const [username, setUsername] = useState('');
  const [balance, setBalance] = useState('');
  const { t, i18n } = useTranslation();

  const fetchUsername = async () => {
    const response = await getUsernameFromApi();
    setUsername(response.data);
  };

  const fetchBalanceData = async () => {
    try {
        const response = await getBalance(fetchBalance);
        setBalance(response.data);
    } catch (error) {
        console.log(error);
    }
  };

  useEffect( () => {
    setVisibleInfo(isVisible);
    setAccountNumber(fetchBalance);
    fetchBalanceData();
    fetchUsername();

    const intervalId = setInterval(() => {
      fetchBalanceData();
      fetchUsername();
    }, 5000);

    return () => clearInterval(intervalId);
  }, []);

  return (
    <div className='header-container'>
      <div className='image-container'>
        <div className='global-container'>
          <Logo/>
          <div className='main-header-div'>
            <h1>MAZE BANK</h1>
            <p>{t('headers.los_santos')}</p>
            {fetchBalance && <p>{t('headers.account_balance')} {balance}</p>}
          </div>
        </div>
        <div className='option-menu'> 
            <ThemeSwitcher/>
            <LangSelector/>
          </div>
      </div>
      <hr className='horizontal-line' />
      <div id="error-content"></div>
      <div id="info-content"></div>
      {messageContent && (
      <>
        <div className="message-content">
          <p>{t('headers.welcome_back')}</p>
          <p>{messageContent}</p>
        </div>
      </>
      )}
      {visibleInfo && (
      <>
        <div className='transaction-content'>
          <div className='transaction-username'>
            <p>{username}</p>
          </div>
          <p className='transaction-maintext'>{headContent}</p>
        </div>
      </>
    )}
    </div>
  );
};

export default Header;
