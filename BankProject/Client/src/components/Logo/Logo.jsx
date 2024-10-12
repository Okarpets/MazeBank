import React from 'react';
import { useThemeStore } from '../../../public/hooks/useTheme';

const Logo = () => {
    const theme = useThemeStore((state) => state.theme);

    const logoSrc = theme === 'dark' ? '/icon-dark.svg' : '/icon-light.svg';

    return <img src={logoSrc} alt="MazeBank Logo" />;
};

export default Logo;