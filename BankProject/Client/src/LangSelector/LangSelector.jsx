import './LangSelector.css'
import { useTranslation } from 'react-i18next';
import { locales } from '../../public/locales/locales';

function LangSelector() {
    const { t, i18n } = useTranslation();

    return (
        <select
          onChange={(e) => i18n.changeLanguage(e.target.value)}
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