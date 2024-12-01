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