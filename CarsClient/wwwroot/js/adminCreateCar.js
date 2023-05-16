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

    // Добавляем два новых элемента input в новый элемент div
    newDiv.appendChild(input3);
    newDiv.appendChild(input1);
    newDiv.appendChild(input2);

    // Добавляем новый элемент div в элемент-родитель
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

function deleteFileHandler() {
    let number = this.attributes['data-number'].value;
    var elem = document.getElementById("selectedFilesForDelete");
    if (elem)
        elem.value += `${this.attributes['data-fileId'].value},`;

    removeFileFromFileList(number);
    this.parentElement.remove();

    var exsImages = document.getElementById("existsImages");
    let fileName = this.attributes['data-filename'].value;
    exsImages.value = exsImages.value;
    const regex = new RegExp(`\\|[^|]*${fileName.replace(".", "\\.")}`);
    exsImages.value = exsImages.value.replace(regex, "");
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