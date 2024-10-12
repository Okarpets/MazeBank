import { useThemeStore } from '../../../../public/hooks/useTheme';
import locales from '../../../../public/locales/locales';
import { useTranslation } from 'react-i18next';
import i18n from '../../../i18n';
import './ThemeSwitcher.css';

function ThemeSwitcher() {
    const { theme, toggleTheme } = useThemeStore();
    const { t, i18n } = useTranslation();
    
    return (
    <>
        <div className='toggle-container'>
            <div className='toggle-theme-text'>
                <p>
                    {theme === "dark" ? (t('themes.dark_mode')) : (t('themes.light_mode'))}
                </p>
            </div>
            <div className='toggle-theme-checkbox'>
                <input onChange={toggleTheme} type="checkbox" id='toggle-btn'/>
                <label htmlFor='toggle-btn' className='toggle-label'></label>
            </div>
        </div>
    </>
    );
};

export default ThemeSwitcher;