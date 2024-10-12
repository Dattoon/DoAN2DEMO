$(".js-addorupdate").click((ev) => {
    $(".modal-AddorUpdate").html("");

    var id = ev.currentTarget.getAttribute("data-id")
    let url = "";
    if (id != null) {
        url = "_Update/" + id;
    }
    else {
        url = "_Create";
    }
    $("#exampleModalLabel").html("Sửa danh mục bài học")
    $.get("/admin/CategorySub/" + url, (ev) => {
        $(".modal-AddorUpdate").append(ev);
    }).then(() => {
        $(".js-addprice").click((ev) => {
            ev.preventDefault();
            var html = `
                <div class="form-group">
                    <label class="control-label">Giá tiền</label>
                    <input name="Price" id="Price" type="text" class="form-control" />
                    <span class="text-danger"></span>
                </div>
            `
            var form = document.querySelector(".js-form");
            form.insertAdjacentHTML("beforeend", html)
        })
    })
    
})

$(".js-postdata").click(() => {
    var data = $(".id-categorysub").val()
    if (data == "") {
        var model = $("form").serialize();
        var formAction = $(this).attr("action");
        var fdata = new FormData();

        var fileInput = $('.image')[0];
        var file = fileInput.files[0];
        fdata.append("Image", file);
        console.log(fdata)

        // You can update the jquery selector to use a css class if you want
        $("input[type='text'").each(function (x, y) {
            fdata.append($(y).attr("name"), $(y).val());
        });
        $("textarea").each(function (x, y) {
            fdata.append($(y).attr("name"), $(y).val());
        });
        // gửi data
        $.ajax({
            type: 'post',
            url: "/admin/CategorySub/_Create",
            data: fdata,
            processData: false,
            contentType: false
        }).done(function (result) {
            // do something with the result now
            if (result == true) {
                window.location.reload();
            }
        });
        //$.post("/admin/CategorySub/_Create", model, (res) => {
        //    if (res == true) {
        //        window.location.reload();
        //    }
        //})
    } else {
        var model = $("form").serialize();
        $.post("/admin/CategorySub/_Update", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        })
    }

})

