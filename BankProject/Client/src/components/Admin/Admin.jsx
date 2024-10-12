import AdminDeleteAccount from './AdminDelete/AdminDeleteAccount';
import AdminDeleteUser from './AdminDelete/AdminDeleteUser';
import AdminDeposite from './AdminOperation/AdminDeposite';
import AdminWithdraw from './AdminOperation/AdminWithdraw';
import locales from '../../../public/locales/locales';
import { useNavigate } from 'react-router-dom';
import { useTranslation } from 'react-i18next';
import AdminRegister from './AdminRegister';
import React, { useState, } from 'react';
import Header from '../Header/Header';
import { Helmet } from 'react-helmet';
import i18n from '../../i18n';

const Admin = () => {
  const [component, setComponent] = useState('');
  const [visible, setVisible] = useState(true);
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const handleExit = async () => {
    localStorage.removeItem('token');
    navigate('/login');
  };

  return (
    <>
      <Helmet>
        <title>MazeBank - {t('admin.admin_panel')}</title>
      </Helmet>
      <div className='background-icons'>
        <div className='main-content'>
          <Header />
          {component}
          {visible && (
            <>
            <div className='input-form'>
              <button onClick={() => {
                setComponent(<AdminDeleteAccount returnVisible={() => { 
                setVisible(true); 
                setComponent(null); }}/>);;
                setVisible(false);
                }}>{t('accounts.delete_account')}</button>
              <button onClick={() => {
                setComponent(<AdminDeleteUser returnVisible={() => { 
                  setVisible(true); 
                  setComponent(null); }}/>);
                setVisible(false);
                }}>{t('admin.delete_user')}</button>
              <button onClick={() => {
                setComponent(<AdminDeposite returnVisible={() => { 
                  setVisible(true); 
                  setComponent(null); }}/>);
                setVisible(false);
                }}>{t('transaction.deposit')}</button>
              <button onClick={() => {
                setComponent(<AdminWithdraw returnVisible={() => { 
                  setVisible(true); 
                  setComponent(null); }}/>);
                setVisible(false);
                }}>{t('transaction.withdraw')}</button>
              <button onClick={ () => {
                setComponent(<AdminRegister returnVisible={() => { 
                  setVisible(true); 
                  setComponent(null); }}/>);
                setVisible(false);
                }}>{t('auth.register')}</button>
              <button onClick={handleExit}>{t('accounts.exit_account')}</button>
              </div> 
            </>
          )}
        </div>
      </div>
    </>
  );
};

export default Admin;
