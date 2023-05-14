$('.delete_car_icon').on('click', function () {
    var carId = this.attributes['data-carId'].value;
    $.ajax({
        url: `/admin/delete/${carId}`,
        type: 'DELETE',
        success: function (result) {
            window.location.reload();
        }
    });
});

document.querySelector("#add_car_block").onclick = function () {
    let elem = document.getElementById('pop_form_wrapper');
    elem.style.visibility = 'visible';
    let elem2 = document.getElementById('body_wrapper');
    elem2.style.opacity = '0.4';
    document.body.style.overflow = 'hidden';
}