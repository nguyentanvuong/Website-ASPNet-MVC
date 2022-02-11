
var contact = {
    init: function () {
        contact.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function () {
            var name = $('#fullname').val();
            var email = $('#email').val();
            var phone = $('#phone').val();
            var title = $('#title').val();
            var detail = $('#detail').val();
            $.ajax({
                url: '/Contact/Send',
                type: 'POST',
                dataType: 'json',
                data: {
                    name: name,
                    email: email,
                    phone: phone,
                    title: title,
                    detail: detail
                },
                success: function (res) {
                    if (res.status == true) {
                        window.alert('Giử thành công');
                        contact.resetForm();
                    }
                }
            });
        });
    },
    resetForm: function () {
        $('#fullname').val('');
        $('#email').val('');
        $('#phone').val('');
        $('#title').val('');
        $('#detail').val('');
    }
}
contact.init();