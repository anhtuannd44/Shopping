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