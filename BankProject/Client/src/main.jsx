import { NotificationProvider } from './components/NotificationProvider/NotificationProvider'
import { createRoot } from 'react-dom/client'
import App from './components/App/App'
import ThemeProvider from './components/ThemeProvider/ThemeProvider'

createRoot(document.getElementById('root')).render(
    <ThemeProvider>
        <NotificationProvider>
            <App />
        </NotificationProvider>
    </ThemeProvider>
)
