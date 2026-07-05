const clubesArgentinos = [
    "Aldosivi", "Argentinos Juniors", "Atlético Tucumán", "Banfield",
    "Barracas Central", "Belgrano", "Boca Juniors", "Central Córdoba (SdE)",
    "Colón", "Defensa y Justicia", "Deportivo Riestra", "Estudiantes de La Plata",
    "Gimnasia y Esgrima (LP)", "Godoy Cruz", "Huracán", "Independiente",
    "Independiente Rivadavia", "Instituto", "Lanús", "Newell's Old Boys",
    "Platense", "Racing Club", "River Plate", "Rosario Central", "San Lorenzo",
    "San Martín (SJ)", "Sarmiento (Junín)", "Talleres", "Tigre", "Unión"
];

document.addEventListener('DOMContentLoaded', () => {
    const selectClub = document.getElementById('club');

    if (selectClub) {
        clubesArgentinos.forEach(club => {
            const opcion = document.createElement('option');
            opcion.value = club;
            opcion.textContent = club;
            selectClub.appendChild(opcion);
        });
    }
});

document.getElementById('registerForm').addEventListener('submit', function (e) {
    e.preventDefault();

    const userData = {
        nombre: document.getElementById('name').value,
        apellido: document.getElementById('lastname').value,
        email: document.getElementById('email').value,
        password: document.getElementById('password').value,
        username: document.getElementById('username').value,
        club: document.getElementById('club').value
    };

    localStorage.setItem('codigoHinchaUser', JSON.stringify(userData));

    window.location.href = 'profile.html';
});