
const selectSingle = document.querySelectorAll('.__select');
const selectSingle_title = document.getElementsByClassName('__select__title');
var titleDiv = document.getElementsByClassName('title_div');
// Toggle menu
var OneSelect;
selectSingle_title[0].addEventListener('click', () => {              
    OneSelect =  selectSingle[0];
    if ('active' === OneSelect.getAttribute('data-state'))  {
        OneSelect.setAttribute('data-state', '');
    }
    else {
        OneSelect.setAttribute('data-state', 'active');
    }            
    let selectSingle_labels = OneSelect.querySelectorAll('.__select__label');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {

      // titleDiv[0].innerHTML = selectSingle_labels[i].getAttribute('data-value');

        selectSingle_title[0].textContent = evt.target.textContent;
        let OneSelect2 =  selectSingle[1];
        let selectSingle_input = OneSelect2.getElementsByClassName('__select__input');    
        if(i!=0)
        {
            for (let k = i-1;k>0;--k){
                selectSingle_input[k].disabled=true;
            }
            for (let k = i;k<selectSingle_labels.length;++k){
                selectSingle_input[k].disabled=false;
            }
        }    
        else
            for(k=0;k<selectSingle_input.length;++k){
                selectSingle_input[k].disabled= false;
            } 
        OneSelect.setAttribute('data-state', '');
    });
    }
})
selectSingle_title[1].addEventListener('click', () => {              
    OneSelect =  selectSingle[1];
    if ('active' === OneSelect.getAttribute('data-state')) {
        OneSelect.setAttribute('data-state', '');
    }
    else {
        OneSelect.setAttribute('data-state', 'active');
    } 
    let selectSingle_labels = OneSelect.querySelectorAll('.__select__label');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {
  
    //    titleDiv[1].innerHTML = selectSingle_labels[i].getAttribute('data-value');
 
        selectSingle_title[1].textContent = evt.target.textContent;
        let OneSelect2 =  selectSingle[0];
        let selectSingle_input = OneSelect2.getElementsByClassName('__select__input');    
        if(i!=0)
        {
            for (let k = i+1;k<selectSingle_labels.length;++k){
                selectSingle_input[k].disabled=true;
            }
            for (let k = i;k>0;--k){
                selectSingle_input[k].disabled=false;
            }
        }   
        else
            for(k=0;k<selectSingle_input.length;++k){
                selectSingle_input[k].disabled= false;
            }  
        
        OneSelect.setAttribute('data-state', '');
    });
    }          

})
selectSingle_title[2].addEventListener('click', () => {              
    OneSelect =  selectSingle[2];
    if ('active' === OneSelect.getAttribute('data-state')) {
        OneSelect.setAttribute('data-state', '');
    }
    else {
        OneSelect.setAttribute('data-state', 'active');
    } 
    let selectSingle_labels = OneSelect.querySelectorAll('.__select__label');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {

   //   titleDiv[2].innerHTML = selectSingle_labels[i].getAttribute('data-value');
 
        selectSingle_title[2].textContent = evt.target.textContent;
        OneSelect.setAttribute('data-state', '');
    });
    }      
    
})


selectSingle_title[3].addEventListener('click', () => {              
    OneSelect =  selectSingle[3];
    if ('active' === OneSelect.getAttribute('data-state')) {
        OneSelect.setAttribute('data-state', '');
    }
    else {
        OneSelect.setAttribute('data-state', 'active');
    } 
    let selectSingle_labels = OneSelect.querySelectorAll('.__select__label');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {

        titleDiv[0].innerHTML = selectSingle_labels[i].getAttribute('data-value');
 
        selectSingle_title[3].textContent = evt.target.textContent;
        let OneSelect2 =  selectSingle[4];
        let selectSingle_input = OneSelect2.getElementsByClassName('__select__input');    
        if(i!=0)
        {
            for (let k = i-1;k>0;--k){
                selectSingle_input[k].disabled=true;
            }
            for (let k = i;k<selectSingle_labels.length;++k){
                selectSingle_input[k].disabled=false;
            }  
        }
        else
            for(k=0;k<selectSingle_input.length;++k){
                selectSingle_input[k].disabled= false;
            }   
            
        OneSelect.setAttribute('data-state', '');
    });
    }
})
selectSingle_title[4].addEventListener('click', () => {              
    OneSelect =  selectSingle[4];
    if ('active' === OneSelect.getAttribute('data-state')) {
        OneSelect.setAttribute('data-state', '');
    }
    else {
        OneSelect.setAttribute('data-state', 'active');
    } 
    let selectSingle_labels = OneSelect.querySelectorAll('.__select__label');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {
  
       titleDiv[1].innerHTML = selectSingle_labels[i].getAttribute('data-value');
 
        selectSingle_title[4].textContent = evt.target.textContent;
        let OneSelect2 =  selectSingle[3];
        let selectSingle_input = OneSelect2.getElementsByClassName('__select__input');    
        if(i!=0) 
        {
            for (let k = i+1;k<selectSingle_labels.length;++k){
                selectSingle_input[k].disabled=true;
            } 
            for (let k = i;k>0;--k){
               selectSingle_input[k].disabled=false;
            } 
        }
        else
            for(k=0;k<selectSingle_input.length;++k){
                selectSingle_input[k].disabled= false;
            }            
        
        OneSelect.setAttribute('data-state', '');
    });
    }          

})