// Declaracion variables localStorage
let acumuladorIntentos = parseInt(localStorage.getItem("intentos"));
let acumuladorNiveles = parseInt(localStorage.getItem("nivel"));
let acumuladorPuntos = parseInt(localStorage.getItem("puntuacion"));

let profileUpdate = {
  id_usuario: localStorage.getItem("idPerfil"),
  nombrePerfil: localStorage.getItem("nombrePerfil"),
  ubicacion: localStorage.getItem("ubicacion"),
  nivel: localStorage.getItem("nivel"),
  intentos: localStorage.getItem("intentos"),
  puntuacion: localStorage.getItem("puntuacion"),
};

// Actualizar Perfil, sumando Puntos, Intentos y Nivel mediante API
function UpdateProfile(score) {

  acumuladorNiveles++;
  localStorage.setItem("nivel", acumuladorNiveles);

  acumuladorPuntos = acumuladorPuntos + score;
  localStorage.setItem("puntuacion", acumuladorPuntos);

  profileUpdate.nivel = localStorage.getItem("nivel");
  profileUpdate.puntuacion = localStorage.getItem("puntuacion");

  console.log(profileUpdate);
}