$(".js-update").click((ev) => {
    $(".modal-Update").html("");
    var id = ev.currentTarget.getAttribute("data-id")
    if (id != null) {
        $("#exampleModalLabel").html("Cập nhật số lượng")
        $.get("/admin/product/_UpdateAmount/" + id, (ev) => {
            $(".modal-Update").append(ev);
        })
    }
})

$(".js-postdata").click(() => {
    var model = $("form").serialize();
    $.post("/admin/product/_UpdateAmount", model, (res) => {
        if (res == true) {
            window.location.href="/admin/product/index";
        }
    })


})