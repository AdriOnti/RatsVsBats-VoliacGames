// Inicializar variables
let succesful = false;
let usuarioId;

// Definir elementos del DOM
const login = document.getElementById("login");

const loginName = document.getElementById("user-name-login");
const loginPass = document.getElementById("user-password-login");
const loginBtn = document.getElementById("loginBtn");


// const mysql = require('mysql');

// const connection = mysql.createConnection({
//   host: 'your-rds-endpoint',
//   user: 'your-username',
//   password: 'your-password',
//   database: 'your-database'
// });

// connection.connect((err) => {
//   if (err) {
//     console.error('Error connecting to database: ' + err.stack);
//     return;
//   }
//   console.log('Connected to database');
// });

// // Select function
// connection.query('SELECT * FROM your_table', (err, rows) => {
//   if (err) {
//     console.error('Error executing query: ' + err.stack);
//     return;
//   }
//   console.log('Data retrieved from database:');
//   console.log(rows);
// });

// // Insert function
// const newRecord = { column1: 'value1', column2: 'value2' };
// connection.query('INSERT INTO your_table SET ?', newRecord, (err, result) => {
//   if (err) {
//     console.error('Error inserting record: ' + err.stack);
//     return;
//   }
//   console.log('New record inserted with ID: ' + result.insertId);
// });

// connection.end();

// Comprobar usuario existente y correcto
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

// Guardar perfil en localStorage
function storageProfile() {
  localStorage.setItem("idPerfil", data.id_usuario);
  localStorage.setItem("nombrePerfil", data.nombrePerfil);
  localStorage.setItem("ubicacion", data.ubicacion);
  localStorage.setItem("puntuacion", data.puntuacion);
  localStorage.setItem("intentos", data.intentos);
  localStorage.setItem("nivel", data.nivel);
}

function popupBlock() {
  if (succesful) {
    document.getElementById("popup-block").style.display = "block";
    document.querySelector("main").style.visibility = "hidden";
  }
}
