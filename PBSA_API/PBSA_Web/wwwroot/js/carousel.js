var slideCount;
var slideWidth
var slideHeight;
var sliderUlWidth;

function initCarousel() {
    slideCount = $('#slider ul li').length;
    slideWidth = $('#slider ul li').width();
    slideHeight = $('#slider ul li').height();
    sliderUlWidth = slideCount * slideWidth;

    $('#checkbox').change(function () {
        setInterval(function () {
            moveRight();
        }, 3000);
    });

    $('#slider').css({ width: slideWidth, height: slideHeight });
    if (slideCount == 1) {
        $('#slider ul').css({ width: sliderUlWidth, marginLeft: 0 });
    } else {
        $('#slider ul').css({ width: sliderUlWidth, marginLeft: - slideWidth });
    }
    $('#slider ul li:last-child').prependTo('#slider ul');
};
function moveLeft() {
    $('#slider ul').animate({
        left: + slideWidth
    }, 200, function () {
        $('#slider ul li:last-child').prependTo('#slider ul');
        $('#slider ul').css('left', '');
    });
};

function moveRight() {
    $('#slider ul').animate({
        left: - slideWidth
    }, 200, function () {
        $('#slider ul li:first-child').appendTo('#slider ul');
        $('#slider ul').css('left', '');
    });
};

$('a.control_prev').click(function () {
    moveLeft();
});

$('a.control_next').click(function () {
    moveRight();
});


