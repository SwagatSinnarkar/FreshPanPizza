﻿//$(document).ready(function () {
//    var cookie = $.cookie('CId');
//    if (cookie) {
//        $.ajax({
//            type: "GET",
//            contentType: "application/json; charset=utf-8",
//            url: '/Cart/GetCartCount',
//            dataType: "json",
//            success: function (data) {
//                $('.noti_Counter').text(data);
//            },
//            error: function (result) {
//            },
//        });
//    }
//});
function AddToCart(ItemId, Name, UnitPrice, Quantity) {

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: '/Cart/AddToCart/' + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (d) {
            var data = JSON.parse(d);
            if (data.Items.length > 0) {
                $('.noti_Counter').text(data.Items.length);
                var message = '<strong>' + Name + '</strong> Added to <a>Cart</a> Successfully!';
                $('#liveToast > .toast-body').html(message);
                $('#liveToast').toast("show");
                setTimeout(function () {
                    $('#liveToast').toast("hide");
                }, 4000);
            }
        },
        error: function (result) {
        }
    });
}

function deleteItem(id) {
    if (id > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: '/Cart/DeleteItem/' + id,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (result) {
            },
        });
    }
}
function updateQuantity(id, quantity) {
    if (id > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: '/Cart/UpdateQuantity/' + id + '/' + quantity,
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (result) {
            },
        });
    }
}

$(document).ready(function () {
    var cookie = $.cookie('CId');
    if (cookie) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: '/Cart/GetCartCount',
            dataType: "json",
            success: function (data) {
                $('.noti_Counter').text(data);
            },
            error: function (result) {
            },
        });
    }
});