function addToCart(thiz) {

    var id = $(thiz).attr("data-id");

    ajaxCart(id, 1);

}
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

function btnAddCart(thiz) {
    var id = $(thiz).attr("data-id");
    var qty = parseInt($('.prd_quantity_' + id).val());
    ajaxCart(id, qty);
}
function ajaxCart(id, qty) {
   
    $.ajax({
        url: "/Order/Add/",
        data: { id: id, qtyPro: qty },
        success: function (response) {
            toastr.success("Đặt hàng thành công");
            $(".cart-count").html(response.Count);


        }

    });
    return false;
}
function changeQuantity(thiz) {
    var valchange;
    var pid = $(thiz).attr("data-id");
    var check = $(thiz).hasClass('btn-minus'),
        val = parseInt($('#qty_' + pid).val());

    if (check) {
        if (val > 1) {
            $('#qty_' + pid).attr('value', val - 1);
            valchange = val - 1;
        }
    } else {
        $('#qty_' + pid).attr('value', val + 1);
        valchange = val + 1;
    }


    $.ajax({
        url: "/Order/Update",
        data: { id: pid, quatity: valchange },
        success: function (response) {
            $(".cart-count").html(response.Count);
            $(".price_end").html(showMoney(response.CartTotal) + " đ");
            $(".cart-price-" + pid).html(showMoney(response.Amount) + " đ");


        }

    });


}

function removeCart(thiz) {
    var id = $(thiz).attr("data-id");
    var tr = $(".productid-" + id);
    $.ajax({
        url: "/Order/Remove/" + id,
        success: function (response) {
            tr.hide(500, function () {
                tr.remove();
            });
            $(".cartCount").html(response.Count);
            $(".totals_price_mobile").html(showMoney(response.CartTotal));
            $(".price_end").html(showMoney(response.CartTotal));
        }
    });

}
function validateOrder() {
    var check = true;
    if ($('.FullName').val() === "") {
        $('.FullName').addClass("validation-failed");
        $('.validateFullName').css("display", "block");
        check = false;
    }
    else {
        $('.FullName').removeClass("validation-failed");
        $('.validateFullName').css("display", "none");
    }
    if ($('.PhoneNumber').val() === "") {
        $('.PhoneNumber').addClass("validation-failed");
        $('.validatePhoneNumber').css("display", "block");
        check = false;
    }
    else {
        $('.PhoneNumber').removeClass("validation-failed");
        $('.validatePhoneNumber').css("display", "none");
    }
    if ($('.Address').val() === "") {
        $('.Address').addClass("validation-failed");
        $('.validateAddress').css("display", "block");
        check = false;
    }
    else {
        $('.Address').removeClass("validation-failed");
        $('.validateAddress').css("display", "none");
    }

    return check;
}
function saveOrder() {
  
    var type = $('input:radio[name=rdPayment]:checked').val();
    var bank = null;
    if (type == "bank") {
        bank = $('input:radio[name=bankcode]:checked').val();
        if (bank == null) {
            toastr.error('Please choose your bank');
            
            return;
        }

    }
    $('#page-preloader').css("display", "block");
    $.ajax({
        type: "POST",
        url: '/Order/CheckOut',
        data: {type: type, bank: bank },
        success: function (data) {
            $('#page-preloader').css("display", "none");
            if (data.Code === 1) {
                window.location = "/Order/Success";
            } else {
                toastr.error(data.message);
            }

        }
    });

}