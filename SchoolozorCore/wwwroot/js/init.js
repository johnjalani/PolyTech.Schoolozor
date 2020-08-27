$(function () {
    var inputs = $('input[data-type]');
    for (var i = 0; i < inputs.length; i++) {
        switch ($(inputs[i]).data('type')) {
            case 'date':
                $(inputs[i]).datepicker({});
                $(inputs[i]).attr('placeholder', 'MM/DD/YYYY');
                break;
            case 'toupper':
                $(inputs[i]).keyup(function () {
                    $(this).val($(this).val().toUpperCase());
                });
                break;
            case 'email':
                $(inputs[i]).inputmask({
                    mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,2}]",
                    greedy: false,
                    onBeforePaste: function (pastedValue, opts) {
                        pastedValue = pastedValue.toLowerCase();
                        return pastedValue.replace("mailto:", "");
                    },
                    definitions: {
                        '*': {
                            validator: "[0-9A-Za-z!#$%&'*+/=?^_`{|}~\-]",
                            casing: "lower"
                        }
                    }
                });
                break;
            case 'phone':
                $(inputs[i]).inputmask('9999999999');
                break;
            case 'mobile':
                $(inputs[i]).inputmask('+999999999999');
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