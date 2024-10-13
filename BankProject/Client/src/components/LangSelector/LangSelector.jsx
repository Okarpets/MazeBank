import React, { useEffect } from 'react';
import locales from '../../../public/locales/locales';
import { useTranslation } from 'react-i18next';
import './LangSelector.css';

function LangSelector() {
    const { i18n } = useTranslation();

    useEffect(() => {
        const savedLanguage = localStorage.getItem('selectedLanguage');
        if (savedLanguage) {
            i18n.changeLanguage(savedLanguage);
        }
    }, [i18n]);

    const handleLanguageChange = (e) => {
        const newLanguage = e.target.value;
        i18n.changeLanguage(newLanguage);
        localStorage.setItem('selectedLanguage', newLanguage);
    };

    return (
        <select className='selector'
            onChange={handleLanguageChange}
            value={i18n.language}
        >
            {Object.entries(locales).map(([key, value]) => (
                <option value={key} key={key}>
                    {value}
                </option>
            ))}
        </select>
    );
}

export default LangSelector;
