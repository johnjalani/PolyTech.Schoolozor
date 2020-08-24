$(function () {
    var inputs = $('input[data-type]');
    for (var i = 0; i < inputs.length; i++) {
        switch ($(inputs[i]).data('type')) {
            case 'datetime':
                $(inputs[i]).datepicker({});
                break;
            default:
                break;
        }
    }
});


//Not yet used
$(function () {
    setTimeout(function () {
        var dates = $('.FormatToDate');
        for (var i = 0; i < dates.length; i++) {
            var dd = moment($(dates[i]).text()).format("LL");
            if (dd !== 'Invalid date') {
                $(dates[i]).text(dd);
            }
        }
    }, 500);
});