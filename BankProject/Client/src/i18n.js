import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import translationRu from '../public/locales/ru/translation.json'; 
import translationEn from '../public/locales/en/translation.json'; 

i18n
  .use(initReactI18next)
  .init({
    resources: {
      ru: {
        translation: translationRu,
      },
      en: {
        translation: translationEn,
      },
    },
    lng: "ru",
    fallbackLng: "en", 
    interpolation: {
      escapeValue: false,
    },
  });

export default i18n;
