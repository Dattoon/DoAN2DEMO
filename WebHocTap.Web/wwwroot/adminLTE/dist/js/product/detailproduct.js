$(".deleteimage").click((ev) => {
    var id = ev.target.getAttribute("data-id")
    $.get("/admin/product/deleteImage/" + id, (res) => {
        if (res) {
            window.location.reload();
        }
    })
})
