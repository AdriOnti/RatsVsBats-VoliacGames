// Definición de elementos del DOM
const tableSelector = document.getElementById("table-ranking");
const records = document.getElementById("records-perfiles");

let perfiles = [];
let posicion = 1;

// Ordenacion de perfiles por puntuacion
function OrdenarPerfiles(perfiles, records) {
  perfiles.sort(function (a, b) {
    if (a.puntuacion > b.puntuacion) {return -1;}
    else if (a.puntuacion < b.puntuacion) {
      return 1;
    } else {
      return a.intentos - b.intentos;
    }
  });
  return perfiles;
}

// Creacion de elementos en el DOM, con datos de la API Perfiles
function CrearPerfiles(perfiles, records) {
  for (const perfil of perfiles) {
    let tr = document.createElement("tr");
    let td1 = document.createElement("td");
    let td2 = document.createElement("td");
    let td3 = document.createElement("td");
    let td4 = document.createElement("td");
    let td5 = document.createElement("td");

    td1.setAttribute("class", "position");
    td2.setAttribute("class", "name");
    td3.setAttribute("class", "country");
    td4.setAttribute("class", "score");
    td5.setAttribute("class", "attemps");

    td1.textContent = posicion;
    td2.textContent = perfil.nombrePerfil;
    td3.textContent = perfil.ubicacion;
    td4.textContent = perfil.puntuacion;
    td5.textContent = perfil.intentos;

    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td3);
    tr.appendChild(td4);
    tr.appendChild(td5);

    records.appendChild(tr);
    posicion++;
  }
}
