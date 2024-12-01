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
        let priceAdded = false;  // Flag to track if price input has been added

        $(".js-addprice").click((ev) => {
            ev.preventDefault();

            // Prevent adding the price section again
            if (priceAdded) {
                return;  // Exit the function if price section is already added
            }

            var html = `
                <div class="form-group">
                    <label class="control-label">Giá tiền</label>
                    <input name="Price" id="Price" type="text" class="form-control" />
                    <span class="text-danger"></span>
                </div>
            `;

            var form = document.querySelector(".js-form");
            form.insertAdjacentHTML("beforeend", html);

            // Set the flag to true so the price section cannot be added again
            priceAdded = true;

            // Optionally, disable the button to prevent clicks
            $(".js-addprice").prop("disabled", true);  // Disables the button
        });
    });
});

$(".js-postdata").click(() => {
    var data = $(".id-categorysub").val();
    if (data == "") {
        var model = $("form").serialize();
        var formAction = $(this).attr("action");
        var fdata = new FormData();

        var fileInput = $('.image')[0];
        var file = fileInput.files[0];
        fdata.append("Image", file);
        console.log(fdata);

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
            if (result == true) {
                window.location.reload();
            }
        });
    } else {
        var model = $("form").serialize();
        $.post("/admin/CategorySub/_Update", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        });
    }
});
$(".js-postdata").click(() => {
    // Get values from the form fields
    var nameCategorySub = $("input[name='NameCategorySub']").val();
    var description = $("textarea[name='Descripstion']").val();
    var image = $("input[name='Image']").val();

    var valid = true;  // Flag to track if the form is valid
    $(".text-danger").remove();  // Clear any previous error messages

    // Check if the 'NameCategorySub' field is empty
    if (nameCategorySub.trim() === "") {
        valid = false;
        $("input[name='NameCategorySub']").after('<span class="text-danger">Danh mục bài học là bắt buộc.</span>');
    }

    // Check if the 'Description' field is empty
    if (description.trim() === "") {
        valid = false;
        $("textarea[name='Descripstion']").after('<span class="text-danger">Mô tả là bắt buộc.</span>');
    }

    // Check if the 'Image' field is empty
    if (image.trim() === "") {
        valid = false;
        $("input[name='Image']").after('<span class="text-danger">Hình ảnh là bắt buộc.</span>');
    }

    // If the form is not valid, prevent submission
    if (!valid) {
        return;  // Stop further execution
    }

    // If the form is valid, proceed with the AJAX submission
    var data = $(".id-categorysub").val();
    if (data == "") {
        var model = $("form").serialize();
        var fdata = new FormData();

        var fileInput = $('.image')[0];
        var file = fileInput.files[0];
        fdata.append("Image", file);
        console.log(fdata);

        $("input[type='text'").each(function (x, y) {
            fdata.append($(y).attr("name"), $(y).val());
        });
        $("textarea").each(function (x, y) {
            fdata.append($(y).attr("name"), $(y).val());
        });

        // Send the form data via AJAX
        $.ajax({
            type: 'post',
            url: "/admin/CategorySub/_Create",
            data: fdata,
            processData: false,
            contentType: false
        }).done(function (result) {
            if (result == true) {
                window.location.reload();
            }
        });
    } else {
        var model = $("form").serialize();
        $.post("/admin/CategorySub/_Update", model, (res) => {
            if (res == true) {
                window.location.reload();
            }
        });
    }
});
// Wait until the document is ready
$(document).ready(function () {
    // Open modal with dynamic content
    function openModal(title, content) {
        // Set modal title and content dynamically
        $('#exampleModalLabel').text(title);
        $('.modal-AddorUpdate').html(content);

        // Show the modal
        $('#modal').modal('show');
    }

    // Handle closing the modal when the "Thoát" button is clicked
    $('.js-close').click(function () {
        $('#modal').modal('hide');
    });

    // Handle form submission when the "Xác nhận" button is clicked
    $('.js-postdata').click(function () {
        // Here you can add your logic to handle data submission.
        // For example, send data to server or manipulate the DOM.

        // Example of alert or logging
        console.log("Data is being submitted...");

        // Close the modal after submission
        $('#modal').modal('hide');
    });

});
document.getElementById("newsForm").onsubmit = function (event) {
    // Clear previous error messages
    document.getElementById('imageError').innerText = '';
    document.getElementById('descriptionError').innerText = '';

    var imageInput = document.getElementById('imageInput');
    var descriptionInput = document.getElementById('descriptionInput');

    // Check if an image is selected
    if (!imageInput.files.length) {
        document.getElementById('imageError').innerText = 'Vui lòng chọn hình ảnh.';
        event.preventDefault(); // Prevent form submission
        return false;
    }

    // Check if description is provided
    if (!descriptionInput.value.trim()) {
        document.getElementById('descriptionError').innerText = 'Vui lòng nhập nội dung mô tả.';
        event.preventDefault(); // Prevent form submission
        return false;
    }

    return true; // Form is valid, allow submission
};