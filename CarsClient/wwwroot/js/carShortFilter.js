document.getElementById('filter_search').addEventListener('click', function () {
    var types = extractValues(document.querySelectorAll('#filter_short_select_types_conainter input:checked'));
    var powerReserves = extractValues(document.querySelectorAll('#filter_select_powerReserves_conainter input:checked'));
    var priceIds = extractValues(document.querySelectorAll('#filter_select_price_conainter input:checked'));

    var params = new URLSearchParams();
    if (powerReserves) addParams(params, 'powerReserves', powerReserves);
    if (types) addParams(params, 'types', types);
    if (priceIds) addParams(params, 'priceIds', priceIds);

    var queryString = String(params);
    var url = '/passenger?' + queryString;

    window.location.href = url; 
});

function addParams(params, name, values) {
    for (var value of values) {
        params.append(name, value);
    }
}

function extractValues(options) {
    var values = [];
    for (let i = 0; i < options.length; i++) {
        values.push(options[i].value);
    }
    return values;
}

const selectSingles = document.querySelectorAll('.__select2');
selectSingles.forEach(selectSingle => {
    const selectSingle_title = selectSingle.querySelector('.__select__title2');
    // Toggle menu
    selectSingle_title.addEventListener('click', () => {
    if ('active' === selectSingle.getAttribute('data-state')) {
        selectSingle.setAttribute('data-state', '');
    } else {
        selectSingles.forEach(selectSingleInternal=> {
            if(selectSingleInternal != selectSingle){
                selectSingleInternal.setAttribute('data-state', '');
            }
        });
        selectSingle.setAttribute('data-state', 'active');
    }
    });

    const selectSingle_labels = selectSingle.querySelectorAll('.__select__label2');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {
            var inputElems =  selectSingle.querySelectorAll('input:checked');
            var checkedCount = inputElems.length;
            var forInput = document.getElementById(evt.target.getAttribute('for'));
            if(forInput.checked){
                if(checkedCount == 1){
                    selectSingle_title.textContent = selectSingle_title.getAttribute('data-default');
                }
                else{

                    if(checkedCount>3){
                        selectSingle_title.textContent = selectSingle_title.getAttribute('data-default') + ', ' + (checkedCount - 1) + ' выбрано';
                    }
                    else if(checkedCount==3){
                        var isFirst = true;
                        for (let i = 0; i < inputElems.length; i++){
                            if(inputElems[i].id == forInput.id){
                                continue;
                            }
                            var selector = '[for="' + inputElems[i].id + '"]';
                            var inputLabel = selectSingle.querySelector(selector);  
                            
                            if(isFirst){
                                selectSingle_title.textContent = inputLabel.textContent;
                                isFirst = false;
                            }
                            else{
                                selectSingle_title.textContent += ', ' + inputLabel.textContent;
                            }                        
                        }                        
                    }
                    else{
                        if(selectSingle_title.textContent.startsWith(evt.target.textContent)){
                            selectSingle_title.textContent = selectSingle_title.textContent.replace(evt.target.textContent + ', ', '');
                        }
                        else{
                            selectSingle_title.textContent = selectSingle_title.textContent.replace(', ' + evt.target.textContent, '');
                        }
                    }
                }
            }
            else{
                if(checkedCount == 0){
                    selectSingle_title.textContent = evt.target.textContent;
                }
                else{
                    if(checkedCount >= 2){
                        selectSingle_title.textContent = selectSingle_title.getAttribute('data-default') + ', ' + (checkedCount + 1) + ' выбрано';
                    }
                    else{
                        selectSingle_title.textContent += ', ' + evt.target.textContent;
                    }
                    
                }
            }
        });
    }
});