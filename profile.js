document.addEventListener('DOMContentLoaded', () => {
    const userData = JSON.parse(localStorage.getItem('codigoHinchaUser'));

    if (!userData) {
        return;
    }

    const nombreEl = document.querySelector('.profile-info h2');
    const usernameEl = document.querySelector('.profile-info .username');

    if (nombreEl) {
        nombreEl.textContent = `${userData.nombre} ${userData.apellido}`;
    }

    if (usernameEl) {
        usernameEl.textContent = `@${userData.username}`;
    }
});