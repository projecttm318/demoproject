function showMoney(input) {
    input += '';
    x = input.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function setIcheck(input, changeInput, valueTrue, valueFalse, callback) {
    $(input).iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });
    $(input).on('ifChecked', function (event) {
        if (typeof changeInput != 'undefined') {
            if (typeof valueTrue != 'undefined') {
                $(changeInput).val(valueTrue);
            } else {
                $(changeInput).val("True");
            }

        }
        if (typeof callback != 'undefined') {
            callback(true, event);
        }

    });

    $(input).on('ifUnchecked', function (event) {
        if (typeof changeInput != 'undefined') {
            if (typeof valueFalse != 'undefined') {
                $(changeInput).val(valueFalse);
            } else {
                $(changeInput).val("False");
            }

        }
        if (typeof callback != 'undefined') {
            callback(false, event);
        }
    });

}