(function () {
    const mediaQuery = window.matchMedia("(min-width: 992px)");

    function handleChange(e) {
        let el = document.getElementById('main-offcanvas-navbar');
        let offcanvas = bootstrap.Offcanvas.getInstance(el);
        if (e.matches) {
            if (offcanvas) {
                if (offcanvas._isShown) {
                    offcanvas.hide();
                }
                else {
                    offcanvas = new bootstrap.Offcanvas(el, { backdrop: false, scroll: true });
                }
                offcanvas.show();
            }
            else {
                offcanvas = new bootstrap.Offcanvas(el, { backdrop: false, scroll: true });
                offcanvas.show();
            }
            document.body.style.marginLeft = getComputedStyle(el).width;
        } else {
            if (offcanvas) {
                offcanvas.hide();
                offcanvas = new bootstrap.Offcanvas(el, { backdrop: true, scroll: false });
            }
            else {
                offcanvas = new bootstrap.Offcanvas(el, { backdrop: true, scroll: false });
                offcanvas.hide();
            }
            document.body.style.marginLeft = "0px";
        }
    }

    // Initial check
    handleChange(mediaQuery);

    // Listen for changes
    mediaQuery.addEventListener("change", handleChange);
})();