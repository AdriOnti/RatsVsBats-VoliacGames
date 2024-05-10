// Comprobación de niveles
if (!(localStorage.key("idProfiles"))) {
  alert("Necesitas loguearte para descargar el juego");
  location.replace("/join-us/");
}