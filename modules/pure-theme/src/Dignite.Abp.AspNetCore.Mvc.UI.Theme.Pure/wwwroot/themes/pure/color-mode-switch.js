
(() => {
    'use strict'

    const getStoredTheme = () => localStorage.getItem('theme')
    const setStoredTheme = theme => localStorage.setItem('theme', theme)

    const getPreferredTheme = () => {
        const storedTheme = getStoredTheme()
        if (storedTheme) {
            return storedTheme
        }

        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
    }

    const setTheme = theme => {
        if (theme === 'auto') {
            document.documentElement.setAttribute('data-bs-theme', (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'))
        } else {
            document.documentElement.setAttribute('data-bs-theme', theme)
        }
    }

    setTheme(getPreferredTheme())

    const showActiveTheme = (theme) => {
        const themeSwitchers = document.querySelectorAll('.color-mode-switch')

        for (var i = 0; i < themeSwitchers.length; i++) {
            var themeSwitcher = themeSwitchers[i];

            const btnDarkMode = themeSwitcher.querySelector('button[data-bs-theme-value="dark"]');
            const btnLightMode = themeSwitcher.querySelector('button[data-bs-theme-value="light"]');

            if (theme == "dark") {
                btnLightMode.classList.remove('d-none');
                btnDarkMode.classList.add('d-none')
            }
            if (theme == "light") {
                btnDarkMode.classList.remove('d-none');
                btnLightMode.classList.add('d-none')
            }
        }
    }

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        const storedTheme = getStoredTheme()
        if (storedTheme !== 'light' && storedTheme !== 'dark') {
            setTheme(getPreferredTheme())
        }
    })

    window.addEventListener('DOMContentLoaded', () => {
        showActiveTheme(getPreferredTheme())

        document.querySelectorAll('[data-bs-theme-value]')
            .forEach(toggle => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value')
                    setStoredTheme(theme)
                    setTheme(theme)
                    showActiveTheme(theme)
                })
            })
    })
})();