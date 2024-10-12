import locales from '../../../public/locales/locales';
import { useTranslation } from 'react-i18next';
import './LangSelector.css'
import i18n from '../../i18n';

function LangSelector() {
    const { t, i18n } = useTranslation();

    return (
        <select className='selector'
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