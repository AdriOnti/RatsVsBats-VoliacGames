
// Definicion elementos DOM
const signup = document.getElementById("signup");
const signupBtn = document.getElementById("signupBtn");

let usuarios = [];

let tempId;

let clickedReg = false;

// Al clickar registrar Usuario
signupBtn.addEventListener("click", (e) => {

  clickedReg = true;

  let user = {
    email: document.getElementById("email").value,
    password: document.getElementById("password").value,
  };

  let profile = {
    id_usuario: tempId + 1,
    nombrePerfil: document.getElementById("nickname").value,
    ubicacion: document.getElementById("location").value,
    nivel: 1,
  };
  
  if (CheckUserRegistered(user.usuario)) {
    console.log("Already Registered With This Email");
    return;
  }

  if(clickedReg) {
    signupBtn.style.pointerEvents = "none";
  }


  // Inserts
  console.log(user);
  console.log(profile);

  signInUser(post);
  signInProfile(put);

  succesful = true;

  setTimeout(function () {
    localStorage.clear();
    popupRegisterSuccesfull();
  }, 1900);
});

function popupRegisterSuccesfull() {
  document.getElementById("popup-block2").style.display = "block";
  document.querySelector("main").style.visibility = "hidden";
}

// PopUp mapa para registrar Ubicacion
function verMapa() {
  window.open("../map/", "_blank", "width=800,height=600");
}

// Recuperar localizacion usuario
window.addEventListener('mouseover', function(event) {
  console.log("mouseover");
  WriteLoc();
});
function WriteLoc(){
  if(localStorage.getItem("locChanged") === "true"){
    document.getElementById("location").value = localStorage.getItem("currentLoc");
  }
}

// Registro Usuario
async function signInUser(post) {
  
}

// Creacion de Perfil
async function signInProfile(put) {
  
}


// Comprobación Usuario registrado
function CheckUserRegistered(user) {
  return usuarios.find(userTarget => userTarget.usuario === user) !== undefined;
}