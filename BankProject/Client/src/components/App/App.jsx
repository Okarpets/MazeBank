import { BrowserRouter as Router } from 'react-router-dom';
import { AuthProvider } from '../AuthContext/AuthContext';
import AppRoutes from './AppRoutes';
import React from 'react';
import ThemeSwitcher from '../ThemeProvider/ThemeSwither/ThemeSwitcher';
import LangSelector from '../LangSelector/LangSelector';
function App() {

  return (
    <AuthProvider>
      <Router>
        <AppRoutes/>
      </Router>
    </AuthProvider>
  );
}

export default App;