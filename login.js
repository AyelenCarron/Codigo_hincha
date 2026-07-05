document.querySelector('form').addEventListener('submit', function (e) {
    e.preventDefault();

    const email = document.getElementById('correo').value;
    const password = document.getElementById('password').value;

    const userData = JSON.parse(localStorage.getItem('codigoHinchaUser'));

    if (userData && userData.email === email && userData.password === password) {
        window.location.href = 'profile.html';
    } else {
        alert('Correo o contraseña incorrectos.');
    }
});