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

