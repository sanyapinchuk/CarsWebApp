document.querySelector("#add_property").onclick = function () {
    var parentElement = document.getElementById("properties_list");

    var allProps = document.getElementsByClassName("onePropName_create");

    var lastProp = allProps[allProps.length - 1].name;
    var lastNumber = +lastProp.substring(4) + 1;
    // Создаем новый элемент div с классом one_property
    var newDiv = document.createElement("div");
    newDiv.classList.add("one_property");

    // Создаем два новых элемента input для нового элемента div
    var input1 = document.createElement("input");
    input1.setAttribute("type", "text");
    input1.setAttribute("placeholder", "Свойство");
    input1.setAttribute("name", "prop" + lastNumber);
    input1.classList.add("onePropName_create");
    input1.classList.add("prop_item_input");

    var input2 = document.createElement("input");
    input2.setAttribute("type", "text");
    input2.setAttribute("placeholder", "Значение");
    input2.setAttribute("name", "val" + lastNumber);
    input2.classList.add("prop_item_input");

    var input3 = document.createElement("input");
    input3.setAttribute("type", "checkbox");
    input3.setAttribute("value", "check" + lastNumber);
    input3.setAttribute("name", "check" + lastNumber);

    var input4 = document.createElement("input");
    input4.setAttribute("type", "text");
    input4.setAttribute("placeholder", "Категория");
    input4.setAttribute("name", "propCategory" + lastNumber);
    input4.classList.add("prop_item_input");

    var input5 = document.createElement("input");
    input5.setAttribute("type", "number");
    input5.setAttribute("placeholder", "Приоритет");
    input5.setAttribute("name", "propCategoryPriority" + lastNumber);
    input5.classList.add("prop_item_input");
    input5.classList.add("prop_item_input_priority");

    var input6 = document.createElement("i");
    input6.classList.add("fas");
    input6.classList.add("fa-times");
    input6.classList.add("delete_property_icon");
    input6.addEventListener('click', deletePropertyHandler);

    newDiv.appendChild(input3);
    newDiv.appendChild(input1);
    newDiv.appendChild(input2);
    newDiv.appendChild(input4);
    newDiv.appendChild(input5);
    newDiv.appendChild(input6);

    parentElement.appendChild(newDiv);
}

document.querySelector("#add_photo").onclick = function () {
    var parentElement = document.getElementById("photos_list");

    var allProps = document.getElementsByClassName("onePhotoName_create");
    var icons = document.getElementsByClassName("delete_file_icon");

    var lastProp = allProps[allProps.length - 1].name;
    var lastNumber = parseInt(lastProp.substring(5)) + 1;
    // Создаем новый элемент div с классом one_property
    var newDiv = document.createElement("div");
    newDiv.classList.add("one_photo");

    var input1 = document.createElement("input");
    input1.setAttribute("type", "text");
    input1.setAttribute("placeholder", "Ссылка");
    input1.setAttribute("name", "photo" + lastNumber);
    input1.classList.add("onePhotoName_create");
    input1.classList.add("photo_item_input");

    var input2 = document.createElement("input");
    input2.setAttribute("type", "radio");
    input2.setAttribute("name", "mainImage");
    input2.setAttribute("value", "photo" + lastNumber);

    var input3 = document.createElement("i");
    input3.setAttribute("data-filename", "photo" + lastNumber);
    input3.classList.add("fas");
    input3.classList.add("fa-times");
    input3.classList.add("delete_file_icon");
    input3.addEventListener('click', deleteFileHandler);
    

    newDiv.appendChild(input2);
    newDiv.appendChild(input1);
    newDiv.appendChild(input3);

    parentElement.appendChild(newDiv);
}

function removeFileFromFileList(index) {
    const dt = new DataTransfer()
    const input = document.getElementById('files_prop')
    const { files } = input

    for (let i = 0; i < files.length; i++) {
        const file = files[i]
        if (index != i)
            dt.items.add(file)
    }

    input.files = dt.files
}

function deletePropertyHandler() {
    if (document.getElementsByClassName("one_property").length > 0) {
        this.parentElement.remove();
    }
}

$('.delete_property_icon').on('click', deletePropertyHandler);

function deleteFileHandler() {
    if (document.getElementsByClassName("one_photo").length > 1) {
        this.parentElement.remove();
    }
}

$('.delete_file_icon').on('click', deleteFileHandler);

$('#files_prop').on('click', function () {
    this.value = "";
});

$('#files_prop').on('change', function () {
    var files = $('#files_prop')[0].files;
    var listElem = $('#exist_files_list');
    var countChilds = $('#exist_files_list>div').length;
    for (let i = 0; i < files.length; i++) {
        listElem.append(`
        <div class="one_file_from_list_wrapper">
            <input type="radio" name="mainImage" value="${files[i].name}">
            <div class="one_file_from_list">${files[i].name}</div>
            <i class="fas fa-times delete_file_icon" data-filename ="${files[i].name}" data-number="${countChilds}"></i>
        </div>
        `)
        countChilds++;
    }
    $('.delete_file_icon').on('click', deleteFileHandler);
});