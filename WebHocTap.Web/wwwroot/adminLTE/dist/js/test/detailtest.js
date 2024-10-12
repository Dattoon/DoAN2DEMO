$(document).ready(() => {
    $(".show-listanswer").click((ev) => {
        ev.preventDefault();
        var id = ev.currentTarget.getAttribute("data-id")
        $("#list-answer").html("");
        var div = document.querySelector("#list-answer");
        $.get("/admin/test/listAnswer/" + id, (res) => {
            for (const item in res) {
                const { description, isSucces, idQues,id } = res[item]
                var html = `
                    <div class="form-group">
                        <lable class="control-label ${colortext(isSucces)}">${description}</lable>
                        <lable class="ml-3" >
                            <a href="/Admin/Test/DeleteAnswer/${id}"
                               class="btn btn-outline-secondary btn-sm"><i class="fas fa-trash"></i></a>
                       
                             <button type="button" data-id="${id}" class="btn js-addorupdate" data-bs-toggle="modal" data-bs-target="#modal">
                                <i class="fas fa-pen"></i>
                            </button>
                        </lable>

                    </div>
                `

                div.insertAdjacentHTML("beforeend", html);

            }
            var btn = `<a href=""  class="sort-listanswer" data-id=${id}>Thu gọn danh sách</a>`
            div.insertAdjacentHTML("afterbegin", btn);
        }).then((res) => {
            $(".js-addorupdate").click((ev) => {
                $(".modal-AddorUpdate").html("");

                var id = ev.currentTarget.getAttribute("data-id")
                if (id != null) {
                    $("#exampleModalLabel").html("Câu trả lời")
                    $.get("/admin/Test/_UpdateAnswer/" + id, (ev) => {
                        $(".modal-AddorUpdate").append(ev);
                    })
                } 
            })
            $(".js-postdata").click(() => {
                var model = $("#form-answer").serialize();
                    $.post("/admin/Test/_UpdateAnswer/", model, (res) => {
                        if (res == true) {
                            window.location.reload();
                        }
                    })
            })
        })
        
    })
    
    const loaddata = (id) => {

    }
    const colortext = (isSucces) => {
        if (isSucces == 1) {
            return "text-success"
        }
        else {
            return ""
        }
    }
})



