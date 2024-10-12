import { getOperationDetail } from '../../../services/apiService';
import locales from '../../../../public/locales/locales';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import i18n from '../../../i18n';
import { jsPDF } from 'jspdf';

const OperationDetail = ({ operationId, returnVisible }) => {
  const [detail, setDetail] = useState('');
  const { t, i18n } = useTranslation();

  useEffect(() => {
    const fetchTransactions = async () => {
      const operationDetail = await getOperationDetail(operationId);
      setDetail(operationDetail.data);
    };

    fetchTransactions();
  }, [operationId]);

  const generatePDF = () => {
    const doc = new jsPDF();

    doc.setFontSize(16);
    doc.text(`${t('transaction.operation_details')}`, 10, 10);
    doc.setFontSize(12);
    doc.text(`${t('transaction.data_id')} ${detail.id}`, 10, 20);
    doc.text(`${t('transaction.data_description')} ${detail.description}`, 10, 30);
    doc.text(`${t('transaction.data_amount')} ${detail.amount}`, 10, 40);

    doc.save('operation-detail.pdf');
  };

  return (
    <>
      <div className='form-content'>
        <div className='input-form'>
          <h2>{t('transaction.operation_details')}</h2>
          <div>
            <p>{t('transaction.data_id')} {detail.id}</p>
            <p>{t('transaction.data_description')} {detail.description}</p>
            <p>{t('transaction.data_amount')} {detail.amount}</p>
          </div>
          <button onClick={returnVisible}>{t('transaction.go_back')}</button>
          <button onClick={generatePDF}>{t('transaction.download_as_pdf')}</button>
        </div>
      </div>
    </>
  );
};

export default OperationDetail;
