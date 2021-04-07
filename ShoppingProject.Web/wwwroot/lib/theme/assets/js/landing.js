(function ($) {
    "use strict";
$('.landing-slider').owlCarousel({
    center: true,
    items:1,
    loop:true,
    margin: 30,
    nav: false,
    autoplay:false,
    autoplayTimeout:5000,
    autoplayHoverPause:false,
    
});

    $('.prooduct-details-box .close').on('click', function (e) {
        var tets = $(this).parent().parent().parent().parent().addClass('d-none');
        console.log(tets);
    })
   })(jQuery);