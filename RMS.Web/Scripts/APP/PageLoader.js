$(window).on(function () {
    $(".loader").fadeOut("slow");
});

! function (a) {
    jQuery(window).bind("unload", function () {
    }), a(document).ready(function () {
        a(".loader").hide(), a("form").on("submit", function () {
            a("form").validate(), a("form").valid() ? (a(".loader").show(), a("form").valid() || a(".loader").hide()) : a(".loader").hide() }), a('a:not([href^="#"])').on("click", function () {
            "" != a(this).attr("href") && a(".loader").show(), a(this).is('[href*="Download"]') && a(".loader").hide();
        }), a("a:not([href])").click(function () {
            a(".loader").hide();
        }), a('a[href*="javascript:void(0)"]').click(function () {
            a(".loader").hide();
        }), a(".export").click(function () {
            setTimeout(function () {
                a(".loader").fadeOut("fast");
            }, 1e3);
        });
    });
}(jQuery);