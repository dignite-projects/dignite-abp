$(function () {
    $('.dropdown-menu a.dropdown-toggle').on('click', function (e) {
        if (!$(this).next().hasClass('show')) {
            $(this).parents('.dropdown-menu').first().find('.show').removeClass("show");
        }

        var $subMenu = $(this).next(".dropdown-menu");
        $subMenu.toggleClass('show');

        $(this).parents('li.nav-item.dropdown.show').on('hidden.bs.dropdown', function (e) {
            $('.dropdown-submenu .show').removeClass("show");
        });

        return false;
    });

    //是否有侧边栏导航
    sideNavbarToggle();
});


//主菜单的切换
window.sideNavbarToggle = () => {
    var $sideNavbar = $('#sideNavbar');
    if ($sideNavbar.length > 0) {
        document.body.classList.remove("no-side-navbar");
    }
    else {
        document.body.classList.add("no-side-navbar");
    }
};

//左侧菜单显示切换
window.sideNavToggle = () => {
    var sideNavbar = document.getElementById('sideNavbar');
    sideNavbar.classList.toggle("show");
};

/**** 页面滚动时隐藏/显示头部 *************/
/**** 在导航中添加 animate__animated animate__fadeInDown sticky-top 样式**********/
$(function () {
    var windowTop = 0;//初始话可视区域距离页面顶端的距离
    var $mainNavbar = $('#main-navbar');
    if ($(window).width() > 576) { //仅在大屏幕设备上执行隐藏\显示主导航设备的动画
        $mainNavbar.addClass("animate__animated");
        $mainNavbar.addClass("animate__fadeInDown");
        $(window).scroll(function () {
            var scrolls = $(this).scrollTop();//获取当前可视区域距离页面顶端的距离
            if (scrolls > windowTop) {//当scrolls>windowTop时，表示页面在向下滑动
                $mainNavbar.css("position", "absolute");

                //需要执行隐藏导航的操作
                if (!$mainNavbar.hasClass('animate__fadeOutUp')) {
                    $mainNavbar.addClass('animate__fadeOutUp');
                    $mainNavbar.removeClass('animate__fadeInDown');
                    $mainNavbar.addClass('bg-white');
                }
                windowTop = scrolls;
            } else {
                $mainNavbar.css("position", "fixed");
                //需要执行显示导航动画操作
                if (!$mainNavbar.hasClass('animate__fadeInDown')) {
                    $mainNavbar.addClass('animate__fadeInDown');
                    $mainNavbar.removeClass('animate__fadeOutUp');
                }
                windowTop = scrolls;

                //
                if (windowTop == 0)
                    $mainNavbar.removeClass('bg-white');
            }
        });
    }
});