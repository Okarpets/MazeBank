import { create } from 'zustand';

const LOCAL_STORAGE_KEY = 'mode';

const getInitialTheme = () => {
  const savedTheme = localStorage.getItem(LOCAL_STORAGE_KEY);
  return savedTheme || 'light';
};

export const useThemeStore = create((set) => ({
  theme: getInitialTheme(),

  setTheme: (newTheme) => {
    localStorage.setItem(LOCAL_STORAGE_KEY, newTheme);
    set({ theme: newTheme });
  },

  toggleTheme: () => set((state) => {
    const newTheme = state.theme === 'light' ? 'dark' : 'light';
    localStorage.setItem(LOCAL_STORAGE_KEY, newTheme);
    return { theme: newTheme };
  })
}));