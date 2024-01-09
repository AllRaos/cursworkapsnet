let menu = document.querySelector('.burger_menu');
let nav = document.querySelector('.navigation');
let dereva = document.querySelector('.paralax_seven');
let sonce = document.querySelector('.paralax_ten');
let gora_one = document.querySelector('.paralax_one');
let gora_two = document.querySelector('.paralax_two');
let gora_three = document.querySelector('.paralax_three');
let gora_four = document.querySelector('.paralax_four');
let oleni = document.querySelector('.paralax_six');
let hmara = document.querySelector('.paralax_eleven');
menu.addEventListener('click', function () {
    this.classList.toggle('active');
    nav.classList.toggle('open');
});

window.addEventListener('scroll', e => {
    let value = window.scrollY;
    let navbar=document.querySelector('.navigation').classList;
    let active_class = "navbar_scrolled";
    let burger_scroll = "burger_scrolled";
   
    if (scrollY > 75 && window.innerWidth > 900) {
        navbar.add(active_class);
    }
    else if (scrollY > 75 && window.innerWidth < 900) {
        menu.classList.add(burger_scroll);
    }
    else {
        navbar.remove(active_class);
        menu.classList.remove(burger_scroll);
    }
    sonce.style.top = value*0.4  + 'px';
    sonce.style.left = value*0.4 + 'px';
    oleni.style.left = value * -0.1 + 'px';
    hmara.style.left = value * -0.6 + 'px';
    gora_two.style.left = value * -0.2 + 'px';
    gora_three.style.left = value * 0.2 + 'px';
    gora_four.style.left = value * -0.2 + 'px';

 })