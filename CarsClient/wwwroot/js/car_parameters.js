document.querySelector("#more").onclick = function(){
    let elem = document.getElementById('add_info');
    elem.style.display= 'initial';
    let elem2 = document.getElementById('less');
    elem2.style.visibility= 'visible';
    let elem3 = document.getElementById('more');
    elem3.style.visibility= 'hidden';			
}
document.querySelector("#less").onclick = function(){
    let elem = document.getElementById('add_info');
    elem.style.display='none';
    let elem2 = document.getElementById('less');
    elem2.style.visibility= 'hidden';
    let elem3 = document.getElementById('more');
    elem3.style.visibility= 'visible';		
}   


document.querySelector("#more_price_button").onclick = function(){
    let elem = document.getElementById('hide_price_info');
    elem.style.display= 'initial';
    let elem2 = document.getElementById('less_price_button');
    elem2.style.visibility= 'visible';
    let elem3 = document.getElementById('more_price_button');
    elem3.style.visibility= 'hidden';			
}
document.querySelector("#less_price_button").onclick = function(){
    let elem = document.getElementById('hide_price_info');
    elem.style.display='none';
    let elem2 = document.getElementById('less_price_button');
    elem2.style.visibility= 'hidden';
    let elem3 = document.getElementById('more_price_button');
    elem3.style.visibility= 'visible';		
}   