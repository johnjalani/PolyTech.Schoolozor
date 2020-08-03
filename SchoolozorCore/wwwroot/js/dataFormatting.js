$(function () {
    setTimeout(function () {
        var datetime = $('.datetime');
        for (var i = 0; i < datetime.length; i++) {
            var value = $(datetime[i]).val();
            $(datetime[i]).val(moment(value).format('lll'));
        }
    }, 500);
})