jQuery(document).ready(function ($) {

	'use-strict';

	$(document).on('scroll', function () {
		// if the scroll distance is greater than 100px
		if ($(window).scrollTop() > 42) {
			// do something
			$('.site-header').addClass('scrolled-header');
		}
		else {
			$('.site-header').removeClass('scrolled-header');
		}
	});

	$(window).load(function () {
		$('.flexslider').flexslider({
			animation: "slide",
			controlNav: "thumbnails"
		});
	});


});
