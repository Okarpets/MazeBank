import { useThemeStore } from "../../../public/hooks/useTheme";
import { useTranslation } from 'react-i18next';
import { locales } from "../../../public/locales/locales";
import './ThemeSwither.css'

function ThemeSwither() {
    const { theme, toggleTheme } = useThemeStore();
    const { t, i18n } = useTranslation();
    
    return (
    <>
        <div className='toggle-container'>
            <div className='toggle-theme-text'>
                <p>
                    {theme === "dark" ? t('main.dark_theme') : t('main.light_theme')}
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

export default ThemeSwither;