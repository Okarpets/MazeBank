import React from "react";
import './DropdownMenu.css'
import { useTranslation } from 'react-i18next';
import { locales } from "../../public/locales/locales";

function DropdownMenu({ children, buttonText }){
  return (
    <div className="dropdown">
      <button className="dropbtn"> {buttonText} </button>
      <div className="dropdown-content">
          { children }
      </div>
    </div>
  );
};

export default DropdownMenu;





  