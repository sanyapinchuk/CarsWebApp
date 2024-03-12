document.getElementById('filter_search').addEventListener('click', function () {
    var manufacturers = extractValues(document.querySelectorAll('#filter_select_manufacturer_conainter input:checked'));
    var types = extractValues(document.querySelectorAll('#filter_select_types_conainter input:checked'));
    var powerReserves = extractValues(document.querySelectorAll('#filter_select_powerReserves_conainter input:checked'));
    var batteryCapacity = extractValues(document.querySelectorAll('#filter_select_batteryCapacity_conainter input:checked'));
    var driveModes = extractValues(document.querySelectorAll('#filter_select_driveModes_conainter input:checked'));
    var priceMin = document.querySelector('#filter_price_min').value;
    var priceMax = document.querySelector('#filter_price_max').value;

    var params = new URLSearchParams();
    if (manufacturers) addParams(params, 'manufactures', manufacturers);
    if (types) addParams(params, 'types', types);
    if (powerReserves) addParams(params, 'powerReserves', powerReserves);
    if (batteryCapacity) addParams(params, 'batteryCapacity', batteryCapacity);
    if (driveModes) addParams(params, 'driveModes', driveModes);
    if (priceMin) params.append('filterPriceMin', priceMin);
    if (priceMax) params.append('filterPriceMax', priceMax);

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

document.getElementById('filter_clear_search').addEventListener('click', function (){
    window.location.href = '/passenger'; 
})

const rangeSliderInit = () => { 
    const range = document.getElementById('range');
    const inputMin = document.getElementById('filter_price_min'); 
    const inputMax = document.getElementById('filter_price_max'); 
  
    if (!range || !inputMin || !inputMax) return 
  
    const inputs = [inputMin, inputMax]; 
    
    noUiSlider.create(range, { 
        start: [inputMin.value, inputMax.value], 
        connect: true, 
        behaviour: 'tap-drag',
        tooltips: true,
        format: wNumb({
            decimals: 0
        }),
        range: { // устанавливаем минимальное и максимальное значения
          'min': 8000,
          'max': 200000
        },
        step: 1000, // шаг изменения значений
      }
    )
    
    range.noUiSlider.on('update', function (values, handle) { // при изменений положения элементов управления слайдера изменяем соответствующие значения
      inputs[handle].value = parseInt(values[handle]);
    });
    
    inputMin.addEventListener('change', function () { // при изменении меньшего значения в input - меняем положение соответствующего элемента управления
      range.noUiSlider.set([this.value, null]);
    });
    
    inputMax.addEventListener('change', function () { // при изменении большего значения в input - меняем положение соответствующего элемента управления
      range.noUiSlider.set([null, this.value]);
    });
    
  }
  
  const init = () => {
    rangeSliderInit() // запускаем функцию инициализации слайдера
  }
  
window.addEventListener('DOMContentLoaded', init) // запускаем функцию init, когда документ будет загружен и готов к взаимодействию
  



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