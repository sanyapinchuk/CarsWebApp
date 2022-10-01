
document.querySelector("#make-order").onclick = function(){
    let elem = document.getElementById('pop-order');
    elem.style.visibility= 'visible';	
    let elem2 = document.getElementById('all-body');
    elem2.style.opacity = '0.4';
    document.body.style.overflow = 'hidden';		
}
document.querySelector("#order-all").onclick = function(){
    let elem = document.getElementById('pop-order');
    elem.style.visibility= 'hidden';	
    let elem2 = document.getElementById('all-body');
      elem2.style.opacity = '1';
    document.body.style.overflow = 'visible';		
}

document.querySelector("#icon-close").onclick = function(){
  let elem = document.getElementById('pop-order');
  elem.style.visibility= 'hidden';	
  let elem2 = document.getElementById('all-body');
    elem2.style.opacity = '1';
  document.body.style.overflow = 'visible';		
}
function myFunction() {
  if (document.selection) { // IE
    var range = document.body.createTextRange();
    range.moveToElementText(document.getElementById("mynumber"));
    range.select();
    document.execCommand("copy");
} else if (window.getSelection) {
    var range = document.createRange();
    range.selectNode(document.getElementById("mynumber"));
   // 
    window.getSelection().addRange(range);
    document.execCommand("copy");
    window.getSelection().removeAllRanges();
}
  
  var tooltip = document.getElementById("myTooltip");
  tooltip.innerHTML = "Скопировано ✔"
}

function outFunc() {
  var tooltip = document.getElementById("myTooltip");
  tooltip.innerHTML = "Копировать";
}