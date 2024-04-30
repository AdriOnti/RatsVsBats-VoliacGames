// Recibir variables de localStorage

let localUsername = localStorage.getItem("nombrePerfil");
let localNivel = localStorage.getItem("nivel");
let localPuntuacion = localStorage.getItem("puntuacion");
let localIntentos = localStorage.getItem("intentos");
let localLocation = localStorage.getItem("ubicacion");

const infoProfileLocal = [
  localUsername,
  localLocation,
  localNivel,
  localPuntuacion,
  localIntentos,
];

// Persistencia del usuario en la web
let usuarioConectado = false;
if (localStorage.key("idPerfil")) {
  usuarioConectado = true;
}

if (usuarioConectado) {
  document.getElementById("verPerfilLink").style.display = "block";
  document.getElementById("logOutButton").style.display = "block";
  document.getElementById("loginButton").style.display = "none";
}

function logout() {
  document.getElementById("verPerfilLink").style.display = "none";
  document.getElementById("logOutButton").style.display = "none";
  localStorage.clear();
  location.reload();
}