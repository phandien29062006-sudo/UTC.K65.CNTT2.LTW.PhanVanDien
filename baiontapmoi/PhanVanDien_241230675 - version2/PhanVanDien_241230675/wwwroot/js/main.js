// AssetHub - hiệu ứng giao diện cơ bản (nút "lên đầu trang")
$(function () {
    $(window).on('scroll', function () {
        $('.to-top').toggle($(this).scrollTop() > 200);
    });
    $('.to-top').on('click', function () {
        $('html, body').animate({ scrollTop: 0 }, 400);
    });
    $('.search-box input').on('focus', function () { $(this).select(); });
});
$(document).on("click",".category-link",function(e){
    e.preventDefault();
    var maLoai=$(this).data("id");
    $.ajax({
        url:"/Home/LocTaiSan",
        type:"Get",
        data:{maLoai:maLoai},
        success:function(result){
            $("#main-content").html(result);
        }
    });
});