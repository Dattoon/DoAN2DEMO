$(".add-size").click((ev) => {
    ev.preventDefault();
    var html =`<div class="form-group">
                        <label class="control-label">Size</label>
                        <input id="Size" name="Size" class="form-control" />
                        <span class="text-danger"></span>
                    </div>`
    var div = document.querySelector(".size");
    div.insertAdjacentHTML("afterbegin", html)
})

$(".add-color").click((ev) => {
    ev.preventDefault();
    var html = `<div class="form-group">
                        <label class="control-label">Màu sắc</label>
                        <input id="Color" name="Color" class="form-control" />
                        <span class="text-danger"></span>
                    </div>
                 <div class="form-group">
                        <label  class="control-label">Số lượng</label>
                        <input id="Amountdesign" name="Amountdesign" class="form-control" />
                        <span class="text-danger"></span>
                    </div> 
`
    var div = document.querySelector(".color");
    div.insertAdjacentHTML("afterbegin", html)
})
$(".add-image").click((ev) => {
    ev.preventDefault();
    var html = `<div class="form-group">
                        <label class="control-label">Hình ảnh</label>
                        <input id="Image" name="Image" type="file" class="form-control" />
                        <span class="text-danger"></span>
                    </div>`
    var div = document.querySelector(".image");
    div.insertAdjacentHTML("afterbegin", html)
})