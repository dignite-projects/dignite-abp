window.blazoriseUi =
{
    /**
     *  Get the visualization height of the first table
     * @param {*} id  In the current page table We need to make table Of id
     * @param {*} extraHeight  Extra height ( The content height at the bottom of the table  Number type , The default is 65)
     */
    getDataGridHeight: function (id, extraHeight) {
        if (typeof extraHeight == "undefined") {
            //   Default bottom pagination 50 +  Margin 10
            extraHeight = 80;

            if (document.body.clientWidth <= 576) {
                //For small screen
                let mainNav = document.getElementById('main-navbar-nav');
                if (mainNav) {
                    extraHeight = extraHeight + mainNav.clientHeight;
                }
            }
        }

        let fixedHeaderTable = document.getElementById(id);
        // The distance from the table content to the top
        let fixedHeaderTableTop = 0
        if (fixedHeaderTable) {
            fixedHeaderTableTop = fixedHeaderTable.getBoundingClientRect().top
        }
        else {
            console.warn("table-fixed-header is not found.Set the initial value of scrolly in the ant table component.");
            return 500;
        }

        // Form height - The height of the top of the table content - The height of the bottom of the table content
        let height = `calc(100vh - ${fixedHeaderTableTop + extraHeight}px)`;
        return height;
    },
};