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
