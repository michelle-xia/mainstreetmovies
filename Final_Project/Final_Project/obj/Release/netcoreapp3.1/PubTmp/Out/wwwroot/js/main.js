// Selects the correct nav-item to active
$(document).ready(function () {
    var path = window.location.pathname;
    $("ul.navbar-nav li a").each(function () {
        if (this.getAttribute("href") == path) {
            $("ul.navbar-nav li").each(function () {
                if ($(this).hasClass("active")) {
                    $(this).removeClass("active");
                }
            });
            $(this).parent().addClass('active');
        }
        else if(this.getAttribute("controller") == path.split("/")[1]){
            $("ul.navbar-nav li").each(function () {
                if ($(this).hasClass("active")) {
                    $(this).removeClass("active");
                }
            });
            $(this).parent().addClass('active');
        }
    });
});

// Fixes Scrollbar with 100vw
function setVw() {
    let vw = document.documentElement.clientWidth / 100;
    document.documentElement.style.setProperty('--vw', `${vw}px`);
    console.log(vw)
}

setVw();
window.addEventListener('resize', setVw);

// Text Animation
$(function () {
    var text = $(".textAnimation");
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();

        if (scroll >= 10) {
            text.removeClass("hidden");
        } else {
            text.addClass("hidden");
        }
    });
});
