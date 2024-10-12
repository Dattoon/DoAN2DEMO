$(".add-answer").click((ev) => {
    ev.preventDefault();
    var html = `<div class="form-group">
                       
                        <input id="AnswerFail" name="AnswerFail" class="form-control" />
                        <span class="text-danger"></span>
                    </div>`
    var div = document.querySelector("#answer");
    div.insertAdjacentHTML("beforeend", html)
})