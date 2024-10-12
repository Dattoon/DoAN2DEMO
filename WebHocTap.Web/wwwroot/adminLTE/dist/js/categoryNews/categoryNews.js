$(".js-addorupdate").click((ev) => {
    $(".modal-AddorUpdate").html("");

    var id = ev.currentTarget.getAttribute("data-id")
    if (id != null) {
        $("#exampleModalLabel").html("Sửa thể loại tin tức")
        $.get("/admin/CategoryNew/_Update/" + id, (ev) => {
            $(".modal-AddorUpdate").append(ev);
        })
    } else {
        $("#exampleModalLabel").html("Thêm thể loại tin tức")
        $.get("/admin/CategoryNew/_Create", (ev) => {
            $(".modal-AddorUpdate").append(ev);
        })
    }
})

$(".js-postdata").click(() => {
    var data = $(".id-category").val()
    if (data == "") {
        var model = $("form").serialize();
        $.post("/admin/CategoryNew/_Create", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        })
    } else {
        var model = $("form").serialize();
        $.post("/admin/CategoryNew/_Update", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        })
    }
    
})