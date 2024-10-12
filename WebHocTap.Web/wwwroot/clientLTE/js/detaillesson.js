$(".js-chapter").click((ev) => { 
    ev.preventDefault();
    var id = ev.currentTarget.getAttribute("data-id");
    $(".js-content").html("");
    var div = document.querySelector(".js-content")
    $.get("/SubjectClient/DetailLesson/" + id, (res) => {
        var array = res.lessons;
        for (let i = 0; i < array.length; i++) {
            div.insertAdjacentHTML("beforeend", array[i].content)
        }
        $(".js-test").html("");
        var test = document.querySelector(".js-test");
        var a = `<a href="/subjectClient/TestClient/${res.idChapter}" > Bài kiểm tra </a>`;
        if (res.isTest) {
            test.insertAdjacentHTML("beforeend", a);
        }
    })
})