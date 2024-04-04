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
});

document.querySelector("#water_div").onclick = function(){
    let menu = document.getElementsByClassName('new_menu')[0];
    let myBody = document.getElementsByTagName('body')[0];
    let headerTop = document.getElementsByClassName('header_top')[0];
    let headerContacts = document.getElementsByClassName('header_contacts')[0];
    let waterDiv = document.getElementById('water_div');
    let menuItems = document.getElementsByClassName('new_back ');
    
    myBody.style.overflow = 'visible';
    menu.style.display = 'none';    
    waterDiv.style.display = 'none';
    headerTop.style.position = 'relative';
    headerTop.style.backgroundColor = 'none';
    headerTop.style.height = '85px';
    headerTop.style.width = 'auto';
    headerContacts.style.width = '100%';
    for (let i = 0; i < menuItems.length; i++) {    
        menuItems[i].style.marginTop = '0';   
        menuItems[i].style.display = 'inline-block';
    }
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
            menuItems[i].style.marginTop = '20px';   
            menuItems[i].style.display = 'block';
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
        for (let i = 0; i < menuItems.length; i++) {    
            menuItems[i].style.marginTop = '0';   
            menuItems[i].style.display = 'inline-block';
        }
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
