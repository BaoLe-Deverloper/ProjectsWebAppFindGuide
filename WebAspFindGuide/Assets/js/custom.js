

$(function() {

  $('.form_datetime').datetimepicker({
       format: "dd/mm/yyyy - hh:ii",
        autoclose: true,
        todayBtn: true,
        pickerPosition: "button-right",
        minuteStep: 30,
        hourStep: 2

  });
});

///Model cho phần Đăng Nhập Đăng Kí///
$('.close').on('click', function(event) {
  event.preventDefault();
   $('#myModal-signUp').css('display','none');
   $('#myModal-Login').css('display','none');
 });

$(window).click(function(event) {
    if ($(event.target).is('#myModal-Login')) {
        
         $('#myModal-Login').css('display','none');
    }
  if ($(event.target).is('#myModal-signUp')) {
         $('#myModal-signUp').css('display','none'); 
    }
});


$('#myBtn-signUp').on('click', function() {
  event.preventDefault();
   $('#myModal-signUp').css('display','block');
});

$('#myBtn-Login').on('click', function() {
  event.preventDefault();
     $('#myModal-Login').css('display','block');
});
///////hết////////
$(function () {
    //Initialize tooltips
    $('.nav-tabs > li a[title]').tooltip();
    
    //Wizard
    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

        var $target = $(e.target);
    
        if ($target.parent().hasClass('disabled')) {
            return false;
        }
    });

    $(".next-step").click(function (e) {

        var $active = $('.wizard .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);

    });
    $(".prev-step").click(function (e) {

        var $active = $('.wizard .nav-tabs li.active');
        prevTab($active);

    });
});
function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}
