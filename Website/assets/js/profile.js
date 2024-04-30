// Mostrar datos pagina perfil
let infoList = document.querySelectorAll("#profileInfo li");
let incrementList = 0;

for (const node of infoList) {
  node.innerText = node.innerText + " " + infoProfileLocal[incrementList];
  incrementList++;
}

let logoutbtn = document.getElementById("logout-btn");
