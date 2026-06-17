document.addEventListener('DOMContentLoaded', () => {
    

    const burgerBtn = document.getElementById('burger');
    const navMenu = document.getElementById('nav-menu');


    if (burgerBtn && navMenu) {
        
        burgerBtn.addEventListener('click', () => {
            
            navMenu.classList.toggle('active');
            
            
            if (navMenu.classList.contains('active')) {
                burgerBtn.textContent = '✕'; 
            } else {
                burgerBtn.textContent = '☰'; 
            }
        });


        document.addEventListener('click', (evento) => {
            if (!burgerBtn.contains(evento.target) && !navMenu.contains(evento.target)) {
                navMenu.classList.remove('active');
                burgerBtn.textContent = '☰';
            }
        });
    }
});