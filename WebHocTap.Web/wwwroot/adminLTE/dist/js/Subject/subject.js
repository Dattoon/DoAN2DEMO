$(".js-addorupdate").click((ev) => {
    $(".modal-AddorUpdate").html("");

    var id = ev.currentTarget.getAttribute("data-id")
    if (id != null) {
        $("#exampleModalLabel").html("Sửa môn học")
        $.get("/admin/Sub/_Update/" + id, (ev) => {
            $(".modal-AddorUpdate").append(ev);
        })
    } else {
        $("#exampleModalLabel").html("Thêm môn học")
        $.get("/admin/Sub/_Create", (ev) => {
            $(".modal-AddorUpdate").append(ev);
        })
    }
})

$(".js-postdata").click(() => {
    var data = $(".id-sub").val()
    if (data == "") {
        var model = $("form").serialize();
        $.post("/admin/Sub/_Create", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        })
    } else {
        var model = $("form").serialize();
        $.post("/admin/Sub/_Update", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        })
    }

})