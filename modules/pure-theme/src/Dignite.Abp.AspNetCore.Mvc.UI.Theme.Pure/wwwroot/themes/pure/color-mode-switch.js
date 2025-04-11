
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


    const showActiveTheme = (theme) => {
        const themeSwitchers = document.querySelectorAll('.color-mode-switch')

        for (var i = 0; i < themeSwitchers.length; i++) {
            var themeSwitcher = themeSwitchers[i];

            const btnDarkMode = themeSwitcher.querySelector('button[data-bs-theme-value="dark"]');
            const btnLightMode = themeSwitcher.querySelector('button[data-bs-theme-value="light"]');

            if (theme == "dark") {
                btnLightMode?.classList.remove('d-none');
                btnDarkMode?.classList.add('d-none')
            }
            else if (theme == "light") {
                btnDarkMode?.classList.remove('d-none');
                btnLightMode?.classList.add('d-none')
            }
        }
    }

    var setBrandLogo = (theme) => {
        if (dignite.brand.logoReverseUrl != '' || dignite.brand.logoUrl != '') {
            const brandImgElement = document.querySelectorAll('.brand-logo');
            for (let item of brandImgElement) {
                if (theme === 'auto') {
                    theme = window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
                }

                if (theme == "dark" && dignite.brand.logoReverseUrl != '') {
                    item.src = dignite.brand.logoReverseUrl;
                    item.classList.remove('d-none');
                }
                if (theme == "light" && dignite.brand.logoUrl != '') {
                    item.src = dignite.brand.logoUrl;
                    item.classList.remove('d-none');
                }
            }
        }
    }

    //Theme switching before the UI code is loaded.
    setTheme(getPreferredTheme());

    //Listening for changes to the color theme preferences of the user's device (e.g. dark mode or light mode)
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        var theme = getStoredTheme();
        if (!theme) {
            theme = (window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
        }
        setTheme(theme)
        showActiveTheme(theme)
        setBrandLogo(theme);
    })

    //Listening for DOM content loading completion events
    window.addEventListener('DOMContentLoaded', () => {
        showActiveTheme(getPreferredTheme());
        setBrandLogo(getPreferredTheme());

        document.querySelectorAll('[data-bs-theme-value]')
            .forEach(toggle => {
                toggle.addEventListener('click', () => {
                    const theme = toggle.getAttribute('data-bs-theme-value')
                    setStoredTheme(theme)
                    setTheme(theme)
                    showActiveTheme(theme)
                    setBrandLogo(theme);
                })
            })
    })
})();