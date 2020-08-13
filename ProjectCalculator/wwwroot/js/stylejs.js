const hamburger = document.querySelector('.hamburger');
const navLinks = document.querySelector('.nav-links');
const links = document.querySelector('.nav-link');
const content = document.querySelector('.content');
const middleList = document.querySelector('.middle-list');
const rightList = document.querySelector('.right-list');
const rightListElements = document.querySelector('.right-list').querySelectorAll("li");

let isMenuOpened = false;

hamburger.addEventListener('click', () => {
    if (isMenuOpened) {
        isMenuOpened = false
    } else {
        isMenuOpened = true;
    }
    navLinks.classList.toggle('nav-active');
    document.body.classList.toggle('lock-scroll');
    content.classList.toggle('curtain');

    console.log(isMenuOpened);

    var movedElementsCount = rightList.length;
    console.log(movedElementsCount);
    if (isMenuOpened) {
        for (var i = 0; i < rightListElements.length; i++) {
            middleList.appendChild(rightListElements[i]);
            console.log(rightListElements[i]);
            console.log(middleList.length);
        }
    }

    if (!isMenuOpened) {


        for (var i = 0; i < rightListElements.length; i++) {
            rightList.appendChild(rightListElements[i]);
        }

    }

});



