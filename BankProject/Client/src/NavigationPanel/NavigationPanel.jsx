import React from "react";
import './NavigationPanel.css'
import DropdownMenu from "../DropdownMenu/DropdownMenu";
import { locales } from "../../public/locales/locales";
import { useTranslation } from 'react-i18next';
import { cards, deposits, transfers, profile } from "../assets/navigation-constants";
import ThemeSwither from "../ThemeProvider/ThemeSwither/ThemeSwither";
import LangSelector from "../LangSelector/LangSelector"; 

function NavigationPanel(){
  const { t, i18n } = useTranslation();

  return (
    <div className="global-navigation">
      <div className="theme-switcher">
        <ThemeSwither/>
      </div>
      <div className="global-navigation_left">
      <DropdownMenu buttonText={t('navigation_left.cards_and_balances')}>
          {cards.map((item, index) => (
            <button className="drop-element" key={index}>
              {t(`cards.${item}`)}
            </button>
          ))}
        </DropdownMenu>

        <DropdownMenu buttonText={t('navigation_left.deposit')}>
          {deposits.map((item, index) => (
            <button className="drop-element" key={index}>
              {t(`deposits.${item}`)}
            </button>
          ))}
        </DropdownMenu>

        <DropdownMenu buttonText={t('navigation_left.transfer')}>
          {transfers.map((item, index) => (
            <button className="drop-element" key={index}>
              {t(`transfers.${item}`)}
            </button>
          ))}
        </DropdownMenu> 
      </div>

      <div className="global-navigation_right">
        <div className="languange-selector">
          <LangSelector/>
        </div>
        <div>
          <DropdownMenu buttonText={t('navigation_right.profile')}>
          {profile.map((item, index) => (
            <button className="drop-element" key={index}>
              {t(`navigation_right.${item}`)}
            </button>
          ))}
          </DropdownMenu>
        </div>
      </div>
    </div>
  );
};

export default NavigationPanel;