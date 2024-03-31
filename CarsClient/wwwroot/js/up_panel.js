

document.querySelector("#arrow_up").onclick = function(){
    window.scrollTo(top);
}

window.addEventListener('scroll', function() {
    let arraw = this.document.getElementById('arrow_up');
    if (pageYOffset > 172)
    {
        arraw.style.opacity = '0.7';
    }  
    if (pageYOffset < 172) {
        arraw.style.opacity = '0';
    } 
    if(document.documentElement.clientWidth>854)
    {
        // to add submenu uncomment elem2 and in cicle change 5 to 8
        if (pageYOffset > 172) {
            let elem = document.getElementById('main_menu');
          //  let elem2 = document.getElementsByClassName('submenu');
           // let elem3 = document.getElementsByClassName('popmenu');
            let active = this.document.getElementsByClassName('active')[0];
            elem.style.backgroundColor='#ffffff';
          //  elem2[0].style.backgroundColor='#9A9A84';

            if (active != null) active.style.boxShadow='-14px -18px 21px -4px rgba(16, 16, 14, 0.21) inset';
            let color_elem = this.document.getElementsByClassName('new_color');
            // if (window.getComputedStyle(color_elem[0]).color== 'rgb(96, 96, 96)')
            for (let i = 0; i < color_elem.length; i++){
                color_elem[i].style.color='#EF7A13';
            } 

        }
        // to add submenu uncomment elem2 and in cicle change 5 to 8
        if (pageYOffset < 172) {
            let elem = document.getElementById('main_menu');
          //  let elem2 = document.getElementsByClassName('submenu');
            let active = this.document.getElementsByClassName('active')[0];
            elem.style.backgroundColor='#EF7A13';
         //   elem2[0].style.backgroundColor='#E3E3DC';

           if (active != null) active.style.boxShadow='-14px -18px 16px -1px rgba(101, 101, 101, 0.21) inset';
           let color_elem = this.document.getElementsByClassName('new_color');

           // if (window.getComputedStyle(color_elem[1]).color=='rgb(80, 67, 21)')
            for (let i = 0; i < color_elem.length; i++){ 
                color_elem[i].style.color='#FFFFFF';
            } 
    
        }
    }
   
});

let my_arrow = document.getElementById('arrow_up');

my_arrow.addEventListener("mouseleave", function () {
    let i_arrow = document.getElementsByClassName('fa-arrow-up')[0]; 
  //  i_arrow.style.top = '90%';
});



//selsect 

function breakOverflow(elm2,parrent) {
    var top = elm2.offsetTop;
    var left = elm2.offsetLeft;
   // elm.appendTo(parrent);
   let elm= elm2.cloneNode(true);
   parrent.append(elm);
   elm.style.position= 'absolute';
   elm.style.left= left+'px';
   elm.style.top= '0';
   elm.style.bottom= 'auto';
   elm.style.right= 'auto';
  /* elm.css({
       position: 'absolute',
       left: left+'px',
       top: top+'px',
       bottom: 'auto',
       right: 'auto',
       'z-index': 10000
    });*/
 } 
 

/*
selectSingle_title[5].addEventListener('click', () => {              
    OneSelect =  selectSingle[5];
    if ('active' === OneSelect.getAttribute('data-state')) {
        OneSelect.setAttribute('data-state', '');
    }
    else {
        OneSelect.setAttribute('data-state', 'active');
    } 
    let selectSingle_labels = OneSelect.querySelectorAll('.__select__label');
    for (let i = 0; i < selectSingle_labels.length; i++) {
        selectSingle_labels[i].addEventListener('click', (evt) => {
  
       titleDiv[2].innerHTML = selectSingle_labels[i].getAttribute('data-value');
 
        selectSingle_title[5].textContent = evt.target.textContent; 
        
        OneSelect.setAttribute('data-state', '');
    });
    }          

})*/
document.querySelector("#water_div").onclick = function(){
    let menu = document.getElementById('main_menu');
    let myBody = document.getElementsByTagName('body')[0];
    let waterDiv = document.getElementById('water_div');
    myBody.style.overflow = 'visible';
    menu.style.display = 'none';    
    waterDiv.style.display = 'none';
}       

document.querySelector("#open_list").onclick = function(){
    let menu = document.getElementsByClassName('new_menu')[0];
    let myBody = document.getElementsByTagName('body')[0];
    let headerTop = document.getElementsByClassName('header_top')[0];
    let headerContacts = document.getElementsByClassName('header_contacts')[0];
    let waterDiv = document.getElementById('water_div');
    let menuItems = document.getElementsByClassName('new_back ');
    if ((menu.style.display == 'none') || (menu.style.display == ''))
    {
        menu.style.position = 'fixed';
        menu.style.display = 'block';
        menu.style.window = '85%';
        menu.style.top = '90px';
        menu.style.left = '50px';
        myBody.style.overflow = 'hidden';
        waterDiv.style.display = 'inline-block';
        for (let i = 0; i < menuItems.length; i++) {    
            menuItems[i].style.display = 'block'
        }
        headerTop.style.position = 'absolute';
        headerTop.style.backgroundColor = '#fafafa';
        headerTop.style.height = '100%';
        headerTop.style.width = '85%';
        headerContacts.style.width = '117.647%';
    }
    else
    {
        myBody.style.overflow = 'visible';
        menu.style.display = 'none';    
        waterDiv.style.display = 'none';
        headerTop.style.position = 'relative';
        headerTop.style.backgroundColor = 'none';
        headerTop.style.height = '85px';
        headerTop.style.width = 'auto';
        headerContacts.style.width = '100%';
    }
    }

let showOptions = document.getElementsByClassName('show_options');       
let hideOptions = document.getElementsByClassName('hide_options');
let subMenus = document.getElementsByClassName('submenu');

for (let i = 0; i < showOptions.length; i++) {    
    showOptions[i].addEventListener('click', (evt) => {
       showOptions[i].style.visibility = 'hidden';       
       hideOptions[i].style.visibility = 'visible';
       subMenus[i].style.display = 'block';       
    });
}
for (let i = 0; i < hideOptions.length; i++) {    
    hideOptions[i].addEventListener('click', (evt) => {
       showOptions[i].style.visibility = 'visible';       
       hideOptions[i].style.visibility = 'hidden';
       subMenus[i].style.display = 'none';       
    });
}

let showOptions2 = document.getElementsByClassName('show_options2');       
let hideOptions2 = document.getElementsByClassName('hide_options2');
let popMenus2 = document.getElementsByClassName('popmenu');

for (let i = 0; i < showOptions2.length; i++) {    
    showOptions2[i].addEventListener('click', (evt) => {
       showOptions2[i].style.visibility = 'hidden';       
       hideOptions2[i].style.visibility = 'visible';
       popMenus2[i].style.display = 'block';       
    });
}
for (let i = 0; i < hideOptions2.length; i++) {    
    hideOptions2[i].addEventListener('click', (evt) => {
       showOptions2[i].style.visibility = 'visible';       
       hideOptions2[i].style.visibility = 'hidden';
       popMenus2[i].style.display = 'none';       
    });
}
