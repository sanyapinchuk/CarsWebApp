
document.querySelectorAll(".delete_car_icon").forEach(el => {
    el.onclick = function () {
        var carId = this.attributes['data-carId'].value;
        $.ajax({
            url: `../admin_8rm7yxmfos9o3bkk3f4he67jn7/delete/${carId}`,
            type: 'DELETE',
            success: function (result) {
                window.location.reload();
            }
        });
    }
})

/*
document.querySelector("#add_car_block").onclick = function () {
    let elem = document.getElementById('pop_form_wrapper');
    elem.style.visibility = 'visible';
    let elem2 = document.getElementById('body_wrapper');
    elem2.style.opacity = '0.4';
    document.body.style.overflow = 'hidden';
}*/