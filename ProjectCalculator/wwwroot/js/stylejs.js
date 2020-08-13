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



