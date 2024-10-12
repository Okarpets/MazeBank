import { useThemeStore } from '../../../public/hooks/useTheme';
import React, { useEffect } from 'react';

const ThemeProvider = ({ children }) => {
    const { theme } = useThemeStore();
  
    useEffect(() => {
      document.body.setAttribute('data-theme', theme);
    }, [theme]);
  
    return <>{children}</>;
  };

export default ThemeProvider;
