
$(function () {
    var url = window.location.pathname;

    $("#navbar ul li a").each(function () {
        if (url.toLowerCase() === $(this).attr("href").toLowerCase()) {
            $(this).addClass("active");
        } else {
            $(this).removeClass("active");
        }
    });
});
