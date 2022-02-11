
var cart = {
    init: function () {
        cart.registerEvents();
    },
    registerEvents: function () {
        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault;
            window.location.href = "/";
        });
        $('#btnThanhToan').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/thanh-toan";
        });
        $('#btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            var listProduct = $('.txtquantity');
            var cartList = [];
            $.each(listProduct, function (i, item) {
                cartList.push({
                    Quantity: $(item).val(),
                    Product: {
                        Id: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                        window.alert('Cập nhật giỏ hàng thành công');
                    }
                }
            })
        });
        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                        window.alert('Xóa giỏ hàng thành công');
                    }
                }
            })
        });
        $('#btnDelete').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: {id: $(this).data('id')},
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                        window.alert('Xóa thành công');
                    }
                }
            })
        });
    }
}
cart.init();