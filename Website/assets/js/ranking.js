// Declaracion de API Perfiles
const url = "https://rvbvoliacgamesapi.azurewebsites.net/api/Profiles";

// Definición de elementos del DOM
const tableSelector = document.getElementById("table-ranking");
const records = document.getElementById("records-perfiles");

let perfiles = [];
let posicion = 1;

// Metodo GET de la API Perfiles
fetch(url)
  .then((response) => {
    console.log(response);
    if (response.ok) {
      return response.json();
    }
  })
  .then((data) => {
    console.log(data);

    for (const perfil of data) {
      perfiles.push(perfil);
    }

    OrdenarPerfiles(perfiles, records);
    CrearPerfiles(perfiles, records);
  })
  .catch((error) => {
    console.log(error);
  });

// Ordenacion de perfiles por puntuacion
function OrdenarPerfiles(perfiles, records) {
  perfiles.sort(function (a, b) {
    if (a.completedMissions > b.completedMissions) {return -1;}
    else if (a.completedMissions < b.completedMissions) {
      return 1;
    } else {
        if (a.points > b.points){
          return -1;
        }
        else if (a.points < b.points)
        {
            return 1;
        }
        else return -1;
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
    td2.textContent = perfil.nickname;
    td3.textContent = perfil.location;
    td4.textContent = perfil.completedMissions;
    td5.textContent = perfil.points;

    tr.appendChild(td1);
    tr.appendChild(td2);
    tr.appendChild(td3);
    tr.appendChild(td4);
    tr.appendChild(td5);

    records.appendChild(tr);
    posicion++;
  }
}