import { PDFDocument, rgb } from 'pdf-lib';
import { getOperationDetail } from '../../../services/apiService';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

const OperationDetail = ({ operationId, returnVisible }) => {
  const [detail, setDetail] = useState({});
  const { t } = useTranslation();


  useEffect(() => {
    const fetchTransactions = async () => {
      const operationDetail = await getOperationDetail(operationId);
      setDetail(operationDetail.data);
    };

    fetchTransactions();
  }, [operationId]);

  const generatePDF = async () => {
    const pdfDoc = await PDFDocument.create();
    const page = pdfDoc.addPage([600, 400]);

    page.drawText("Transaction details", {
      x: 10,
      y: 370,
      size: 16,
      color: rgb(0, 0, 0),
    });

    page.drawText(`ID: ${detail.id}`, {
      x: 10,
      y: 350,
      size: 12,
      color: rgb(0, 0, 0),
    });

    let transactionTypeText;

    switch(detail.transactionType) {
      case 1:
        transactionTypeText = "Deposit";
        break;
      case 2:
        transactionTypeText = "Withdraw";
        break;
      case 3:
        transactionTypeText = "Transfer";
        break;
      default:
        transactionTypeText = "Unknown";
        break;
    }
    
    page.drawText(`Type: ${transactionTypeText}`, {
      x: 10,
      y: 330,
      size: 12,
      color: rgb(0, 0, 0),
    });

    if (detail.transactionType === 3) {
      page.drawText(`From: ${detail.fromAccountNumber}`, {
        x: 10,
        y: 310,
        size: 12,
        color: rgb(0, 0, 0),
      });
      page.drawText(`To: ${detail.toAccountNumber}`, {
        x: 10,
        y: 290,
        size: 12,
        color: rgb(0, 0, 0),
      });
    }

    page.drawText(`Date: ${detail.transactionDate}`, {
      x: 10,
      y: 250,
      size: 12,
      color: rgb(0, 0, 0),
    });

    page.drawText(`Amount: ${detail.amount}`, {
      x: 10,
      y: 230,
      size: 12,
      color: rgb(0, 0, 0),
    });

    const pdfBytes = await pdfDoc.save();
    const blob = new Blob([pdfBytes], { type: 'application/pdf' });
    const link = URL.createObjectURL(blob);

    const a = document.createElement('a');
    a.href = link;
    a.download = 'operation-detail.pdf';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  };

  return (
    <div className='form-content'>
      <div className='input-form'>
        <h2>{t('transaction.operation_details')}</h2>
        <div>
          <p>{t('transaction.data_id')} {detail.id}</p>
          <p>{t('transaction_data.type')} {t(`transaction_data.${detail.transactionType}`)}</p>
          {detail.transactionType === 3 && (
            <>
              <p>{t('transaction_data.from')} {detail.fromAccountNumber}</p>
              <p>{t('transaction_data.to')} {detail.toAccountNumber}</p>
            </>
          )}
          <p>{t('transaction_data.date')} {detail.transactionDate}</p>
          <p>{t('transaction.data_amount')} {detail.amount}</p>
        </div>
        <button onClick={returnVisible}>{t('transaction.go_back')}</button>
        <button onClick={generatePDF}>{t('transaction.download_as_pdf')}</button>
      </div>
    </div>
  );
};

export default OperationDetail;
