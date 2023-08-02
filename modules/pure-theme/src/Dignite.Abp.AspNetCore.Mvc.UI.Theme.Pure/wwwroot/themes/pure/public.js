window.dignite =
{
    /**
     * calculate full screen element height
     * @param {any} element
     * @param {any} extraHeight
     * @returns
     */
    calculateFullScreenElementHeight: function (element, extraHeight = 0) {
        // The distance from the table content to the top
        let topMarginHeight = element.getBoundingClientRect().top;

        // Form height - The height of the top of the table content - The height of the bottom of the table content
        let height = `calc(100vh - ${topMarginHeight + extraHeight}px)`;
        return height;
    }
};

/**
 * Relevant code for the main navigation bar
 */
$(document).ready(function () {
    var navbarContainer = document.getElementById('main-navbar-container');//Get the navigation bar container
    var navbar = navbarContainer.getElementsByClassName('navbar');//Get the navigation bar
    var overlay = document.getElementById('navbar-overlay'); // Get the navigation bar overlay
    var dropdowns = document.querySelectorAll('#main-navbar-collapse > .navbar-nav > .nav-item > .dropdown'); //Get every menu in the navigation bar, excluding submenus

    /**
     * Whether the menu under the navigation bar is expanded. Expand without submenus¡£
     * If expanded, the navigation bar height does not need to be reset when another navigation menu is expanded
     */
    var navbarExpanded = false; 
    var expandedDropdown = null; //Expanded navbar menu
    
    dropdowns.forEach(function (dropdown) {
        /** Event to expand the navbar menu*/
        dropdown.addEventListener('shown.bs.dropdown', event => {
            //If it is on the mobile side, there is no need to set the height of the navigation bar, and return directly here
            if (document.body.scrollWidth < 992) {
                return;
            }
            var dropdownMenu = event.currentTarget.querySelector('.dropdown-menu');
            var height = dropdownMenu.offsetHeight + 100;    // Set the height of the navigation bar menu element
            navbarContainer.style.setProperty('--navbar-horizontal-height', height + 'px');

            // expand menu
            if (!navbarContainer.classList.contains('has-subnav-open')) {
                navbarContainer.classList.add('has-subnav-open');
                overlay.classList.add('show');
            }

            /**
             * Determine whether the expanded navigation bar menu
             * If it is a submenu, navbarExpanded = false
             */
            if (event.target.parentElement.classList.contains('dropdown-submenu')) {
                navbarExpanded = false;
            }
            else {
                navbarExpanded = true;
            }
            expandedDropdown = event.target;
        });

        /** Event to close the menu*/
        dropdown.addEventListener('hidden.bs.dropdown', event => {
            /**
             * If a navbar menu is open, do not reset the height of the navbar when opening another menu.
             */
            if ((!navbarExpanded && !event.target.parentElement.classList.contains('dropdown-submenu'))
                || (expandedDropdown == event.target)) {
                navbarContainer.style.removeProperty('--navbar-horizontal-height');
                navbarContainer.classList.remove('has-subnav-open');
                overlay.classList.remove('show');
            }

            navbarExpanded = false;
            expandedDropdown = null;
        });
    });

    /**
     * Prevent submenu events from bubbling up
     */
    $(navbar).find('.dropdown-submenu a.dropdown-toggle').on('click', function (e) {
        e.stopPropagation();
    });
});
