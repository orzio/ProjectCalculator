    const hamburger = document.querySelector('.hamburger');
    const navLinks = document.querySelector('.nav-links');
    const links = document.querySelector('.nav-link');
    const content = document.querySelector('.content');

    hamburger.addEventListener('click', () => {
        navLinks.classList.toggle('nav-active');
        document.body.classList.toggle('lock-scroll');
        content.classList.toggle('curtain');
        
    });

