// Inicializar variables
let successful = false;
let userId;

// Definir elementos del DOM
const loginName = document.getElementById("login-email");
const loginPass = document.getElementById("login-password");
const loginBtn = document.getElementById("loginBtn");
const feedback = document.getElementById("feedback");

const mysql = require('mysql');

const connection = mysql.createConnection({
  host: 'rats-vs-bats-db.ctusuewsqph4.us-east-1.rds.amazonaws.com',
  user: 'developer',
  password: 'adminVoliac13',
  database: 'RatsVsBats'
});

connection.connect((err) => {
  if (err) {
    console.error('Error connecting to database: ' + err.stack);
    return;
  }
  console.log('Connected to database');
});

// Select function
connection.query('SELECT * FROM Users', (err, rows) => {
  if (err) {
    console.error('Error executing query: ' + err.stack);
    return;
  }
  console.log('Data retrieved from database:');
  console.log(rows);
  setTimeout(closeCon, 5000);
});

function closeCon()
{
  connection.end();
}

function RecogerIdUsuario(usuariosLogin) {
  for (const usuario of usuariosLogin) {
    if (
      usuario.usuario === loginName.value &&
      usuario.password === loginPass.value
    ) {
      console.log(usuario);
      console.log("Hola campeon");
      succesful = true;
      usuarioId = usuario.id;
      break;
    }
  }

  if (!succesful) {
    console.log("Usuario o contraseña incorrectos");
    loginPass.value = "";
  }
  
}

// Agregar evento de clic al botón de inicio de sesión
loginBtn.addEventListener("click", async function() {
  // Obtener los valores de entrada del usuario
  const email = loginName.value.trim();
  const password = loginPass.value.trim();

  // Resetear mensajes de feedback
  feedback.innerHTML = "";

  // Validar que se hayan ingresado nombre de usuario y contraseña
  if (!email) {
    feedback.innerHTML = "Por favor, ingresa tu correo electrónico.";
    return;
  }

  if (!password) {
    feedback.innerHTML = "Por favor, ingresa tu contraseña.";
    return;
  }

  try {
    // Comprobar si el correo electrónico existe en la tabla Users
    const userData = await checkUserExists(email);
    if (!userData) {
      feedback.innerHTML = "El correo electrónico ingresado no está registrado.";
      return;
    }

    // Validar la contraseña
    const isValidPassword = await checkPassword(email, password);
    if (!isValidPassword) {
      feedback.innerHTML = "La contraseña ingresada es incorrecta.";
      return;
    }

    // Obtener el ID del usuario
    userId = userData.idUsers;

    // Obtener el perfil del usuario desde la tabla Profiles
    const userProfile = await getUserProfile(userId);
    if (!userProfile) {
      feedback.innerHTML = "Error al obtener el perfil del usuario.";
      return;
    }

    // Almacenar los datos del perfil del usuario en el almacenamiento local
    storeUserProfileInLocalStorage(userProfile);

    // Redirigir a la página de perfil
    window.location.href = "perfil.html";

  } catch (error) {
    console.error("Error al iniciar sesión:", error);
    feedback.innerHTML = "Error al iniciar sesión. Por favor, inténtalo de nuevo más tarde.";
  }
});

// Función para comprobar si el correo electrónico existe en la tabla Users
async function checkUserExists(email) {
  // Aquí deberías realizar una consulta a la base de datos para verificar si el correo electrónico existe en la tabla Users
  // Si existe, deberías devolver los datos del usuario
  // Si no existe, deberías devolver null
  // Simulamos la respuesta con una promesa
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      // Simulamos que el correo electrónico existe en la tabla Users y devolvemos los datos del usuario
      resolve({
        idUsers: 123, // ID del usuario
        // Otros datos del usuario...
      });
    }, 1000); // Simulamos una demora de 1 segundo
  });
}

// Función para validar la contraseña
async function checkPassword(email, password) {
  // Aquí deberías realizar una consulta a la base de datos para validar la contraseña del usuario
  // Si la contraseña es válida, deberías devolver true
  // Si la contraseña no es válida, deberías devolver false
  // Simulamos la respuesta con una promesa
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      // Simulamos que la contraseña es válida para el usuario con el correo electrónico proporcionado
      resolve(true);
    }, 1000); // Simulamos una demora de 1 segundo
  });
}

// Función para obtener el perfil del usuario desde la tabla Profiles
async function getUserProfile(userId) {
  // Aquí deberías realizar una consulta a la base de datos para obtener el perfil del usuario desde la tabla Profiles
  // Si se encuentra el perfil del usuario, deberías devolver los datos del perfil
  // Si no se encuentra el perfil del usuario, deberías devolver null
  // Simulamos la respuesta con una promesa
  return new Promise((resolve, reject) => {
    setTimeout(() => {
      // Simulamos que encontramos el perfil del usuario y devolvemos los datos del perfil
      resolve({
        // Datos del perfil del usuario...
      });
    }, 1000); // Simulamos una demora de 1 segundo
  });
}

// Función para almacenar los datos del perfil del usuario en el almacenamiento local
function storeUserProfileInLocalStorage(userProfile) {
  // Almacenar los datos del perfil del usuario en el almacenamiento local
  localStorage.setItem("userId", userProfile.idUsers);
  // Otros campos del perfil del usuario...
}






// // Guardar perfil en localStorage
// function storageProfile() {
//   localStorage.setItem("idPerfil", data.id_usuario);
//   localStorage.setItem("nombrePerfil", data.nombrePerfil);
//   localStorage.setItem("ubicacion", data.ubicacion);
//   localStorage.setItem("puntuacion", data.puntuacion);
//   localStorage.setItem("intentos", data.intentos);
//   localStorage.setItem("nivel", data.nivel);
// }

// function popupBlock() {
//   if (succesful) {
//     document.getElementById("popup-block").style.display = "block";
//     document.querySelector("main").style.visibility = "hidden";
//   }
// }
