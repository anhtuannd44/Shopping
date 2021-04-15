// Write your JavaScript code.
function deleteAjax(id, url) {
    var url = url + "?id=" + id;
    Swal.fire({
        title: 'Bạn thực sự muốn xóa?',
        text: "Sau khi \"Đồng ý\" sẽ không thể hoàn tác!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Đồng ý',
        showLoaderOnConfirm: true,
        preConfirm: function () {
            $.ajax({
                method: "POST",
                url: url,
                beforSend: function () {
                    loadSpinner()
                },
                success: function (response) {
                    var data = response;
                    console.log(data);
                    if (data.Status == 200) {
                        Swal.fire(
                            'Thành công!',
                            'Dữ liệu đã được xóa.',
                            'success'
                        );
                        $("#td_" + id).remove();
                    }
                    else {
                        Swal.fire(
                            'Thất bại!',
                            'Có lỗi xảy ra trong quá trình thực hiện. Vui lòng thử lại.',
                            'error'
                        )
                    }
                },
                error: function () {
                    Swal.fire(
                        'Thất bại!',
                        'Có lỗi xảy ra khi gửi dữ liệu.',
                        'error'
                    )
                },
                complete: function () {
                    exitSpinner()
                }
            })
        }
    })
}
function loadSpinner() {
    $('.loader-wrapper').fadeIn(500);
}
function exitSpinner() {
    $('.loader-wrapper').fadeOut(500);
}

function sendSuccess(data) {
    if (data.status == "success") {
        Swal.fire({
            position: 'center',
            icon: 'success',
            title: 'Thành công!',
            text: data.message,
            showConfirmButton: false,
            timer: 1500
        });
        setTimeout(function () {
            window.location.reload()
        }, 1500)
    }
    else {
        Swal.fire({
            position: 'center',
            icon: 'error',
            title: 'Thất bại!',
            text: data.message,
            showConfirmButton: false,
            timer: 2000
        })
    }
}
function sendFalure() {
    Swal.fire({
        position: 'center',
        icon: 'error',
        title: 'Thất bại!',
        text: 'Có lỗi khi gửi dữ liệu, vui lòng thử lại',
        showConfirmButton: false,
        timer: 2000
    })
}
$("#dangKyTuVan").on("click", function (e) {
    $.ajax({
        type: "POST",
        url: "/Home/TuVanSucKhoe",
        data: {
            Email: $("#tuVanEmail").val(),
            Gentle: $("#tuVanGioiTinh").val(),
            BirthYear: $("#tuVanNamSinh").val()
        },
        dataType: "json",
        beforeSend: function () {
            loadSpinner();
        },
        success: function (response) {
            Swal.fire(
                'Thành công!',
                'Thông tin của bạn đã được ghi nhận, chúng tôi sẽ gửi tư vấn cho bạn sớm nhất!',
                'success'
            )
            exitSpinner();
        },
        error: function (response) {
            var message = "";
            if (response.responseJSON != null) {
                $.each(response.responseJSON, function (i, item) {
                    message += "<p>" + response.responseJSON[i].ErrorMessage + "</p>";
                })
            }
            else {
                message = "Có lỗi xảy ra, vui lòng thử lại"
            }
            Swal.fire(
                'Thất bại!',
                message,
                'error'
            )
            exitSpinner();
        }
    });
})

$(document).ready(function () {
    loadMiniCart();
    exitSpinner();
    window.onscroll = function () { myFunction() };

    var header = document.getElementById("mainHeader");
    var sticky = header.offsetTop;
    function myFunction() {
        if (window.pageYOffset > sticky) {
            header.classList.add("fixed-top");
        } else {
            header.classList.remove("fixed-top");
        }
    }

    //Active HeaderMenu
    var currentHref = window.location.pathname;
    $("#headerMenu li").each(function (e, index) {
        var href = $(this).children("a").first().attr("href");
        if (href == currentHref) {
            $(this).children("a").first().addClass("active")
        }
    })
});
function loadMiniCart() {
    $("#miniCart").load("/Cart/MiniCart");
}
$(document).on("click", ".remove-product", function () {
    var productId = $(this).data("id");
    $.ajax({
        method: "GET",
        url: "/cart/remove",
        data: {
            productId: productId
        },
        beforeSend: function () {
            loadSpinner();
        },
        success: function (response) {
            var data = response;
            if (data.Status == 200) {
                loadMiniCart();
                Swal.fire(
                    'Thành công!',
                    'Xóa sản phẩm khỏi giỏ hàng',
                    'success'
                );
            }
            else {
                Swal.fire(
                    'Thất bại!',
                    'Có lỗi xảy ra trong quá trình thực hiện. Vui lòng thử lại.',
                    'error'
                )
            }
        },
        error: function () {
            Swal.fire(
                'Thất bại!',
                'Có lỗi xảy ra khi gửi dữ liệu.',
                'error'
            )
        },
        complete: function () {
            exitSpinner()
        }
    })
})