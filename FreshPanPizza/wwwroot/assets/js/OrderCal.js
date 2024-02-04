var currencySign = "₹";
var taxRate = 0.125;

//Bootstrap-TouchSpin
$("input[name='demo_vertical']").TouchSpin({
    verticalbuttons: true
});

$("input[name='demo_vertical']").on('change', function (event) {

    // console.log(event.currentTarget.value)
    updateQuantity(event.currentTarget);
})

