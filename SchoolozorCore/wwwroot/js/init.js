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
            case 'zip':
                $(inputs[i]).inputmask('99999');
                break;

            default:
                break;
        }
    }

    var selects = $('select[data-type]');
    for (var i = 0; i < selects.length; i++) {
        switch ($(selects[i]).data('type')) {
            case 'country':
                var selectedValue = $(selects[i]).data('selectedvalue');
                $(selects[i]).append('<option value="">Select</option>');
                for (var j = 0; j < GetCountries().length; j++) {
                    if (selectedValue != undefined && GetCountries()[j].value == selectedValue) {
                        $(selects[i]).append('<option selected value="' + GetCountries()[j].value + '">' + GetCountries()[j].name + '</option>');
                    } else {
                        $(selects[i]).append('<option value="' + GetCountries()[j].value + '">' + GetCountries()[j].name + '</option>');
                    }
                }

                break;
        }
    }

    var buttons = $('button[data-type]');
    for (var i = 0; i < buttons.length; i++) {
        switch ($(buttons[i]).data('type')) {
            case 'confirm':
                $(buttons[i]).easyconfirm();
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