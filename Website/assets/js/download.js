// Comprobación de niveles
if (!(localStorage.key("idPerfil"))) {
  alert("Necesitas loguearte para descargar el juego");
  location.replace("/join-us/");
}

