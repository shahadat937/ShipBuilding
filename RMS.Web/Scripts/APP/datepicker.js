
$(function() {
    $(".datepicker").datepicker({
        dateFormat: 'dd MM yy',
        yearRange: '1920:' + new Date().getFullYear(),
        changeMonth: true,
        changeYear: true,
    });
})
