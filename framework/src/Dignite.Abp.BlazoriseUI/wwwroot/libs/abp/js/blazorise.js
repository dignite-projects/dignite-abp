window.blazoriseUi =
{
    /**
     *  Get the visualization height of the first table
     * @param {*} id  In the current page table We need to make table Of id
     * @param {*} extraHeight  Extra height ( The content height at the bottom of the table  Number type , The default is 65)
     */
    getDataGridHeight: function (id, extraHeight) {
        if (typeof extraHeight == undefined || extraHeight=="undefined") {
            //Default bottom pagination 50 +  Margin 10
            extraHeight = 115;

            if (document.body.clientWidth <= 576) {
                //For small screen
                let mainNav = document.getElementById('main-navbar-nav');
                if (mainNav) {
                    extraHeight = extraHeight + mainNav.clientHeight;
                }
            }
        }

        return window.blazoriseUi.calculateFullScreenElementHeight(id, extraHeight);
    },

    calculateFullScreenElementHeight: function (id, extraHeight) {
        let element = document.getElementById(id);
        // The distance from the table content to the top
        let topMarginHeight = 0
        if (element) {
            topMarginHeight = element.getBoundingClientRect().top
        }
        else {
            console.warn("'element' is not found.");
            return 500;
        }

        // Form height - The height of the top of the table content - The height of the bottom of the table content
        let height = `calc(100vh - ${topMarginHeight + extraHeight}px)`;
        return height;
    }
};