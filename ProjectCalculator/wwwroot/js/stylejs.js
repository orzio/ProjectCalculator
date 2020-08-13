const hamburger = document.querySelector('.hamburger');
const navLinks = document.querySelector('.nav-links');
const links = document.querySelector('.nav-link');
const content = document.querySelector('.content');
const middleList = document.querySelector('.middle-list');
const rightList = document.querySelector('.right-list');
const rightListElements = document.querySelector('.right-list').querySelectorAll("li");

let isMenuOpened = false;

hamburger.addEventListener('click', () => {

    isMenuOpened = isMenuOpened == true ? false : true;

    navLinks.classList.toggle('nav-active');
    document.body.classList.toggle('lock-scroll');
    content.classList.toggle('curtain');

    if (isMenuOpened)
        addNavElements();

    if (!isMenuOpened)
        deleteNavElements();

});

function addNavElements() {
    for (var i = 0; i < rightListElements.length; i++) {
        middleList.appendChild(rightListElements[i]);

    }
}

function deleteNavElements() {

    for (var i = 0; i < rightListElements.length; i++) {
        rightList.appendChild(rightListElements[i]);
    }
}


document.body.onload = addElement;

function addElement() {
    // create a new div element 
    // and give it popup content 
    var newDiv = document.createElement("div");
    newDiv.innerHTML += '<button class="open_button" onClick="openPopup()">Open Popup</button><div id="popup" style="  position: absolute;width: 300px;z-index: 999;display: none;top:0;background-color: #fff;  border: 1px solid #ddd;  border-radius: 5px;  box-shadow: 0 2px 8px #aaa;  overflow: hidden;   padding: 10px;"><div class="popup_body" style="  height: 100px;">This is sample popuup</div><button class="close_button"onClick="closePopup()">close</button</div>';

    // add the newly created element and its content into the DOM 
    var currentDiv = document.getElementById("main_container");
    document.body.insertBefore(newDiv, currentDiv);

    // open popup onload
    openPopup();
}

function openPopup() {
    var el = document.getElementById('popup');
    el.style.display = 'block';

    // Updates: set window background color black
    document.body.style.background = '#353333';
}

function closePopup() {
    var el = document.getElementById('popup');
    el.style.display = 'none';
    document.body.style.background = 'white';
}

