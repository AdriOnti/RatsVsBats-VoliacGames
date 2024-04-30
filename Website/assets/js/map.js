let latUser;
let longUser;

var marker;

// Deficion de mapa en posicion actual
var map = L.map("map").setView([41.45361795384844, 2.1863831892761048], 4);

L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
  maxZoom: 19,
  attribution:
    '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
}).addTo(map);

// Al hacer click en el mapa llama a getlocation()
map.on("click", function (e) {
  if (marker) map.removeLayer(marker);
  console.log(e.latlng); 
  marker = L.marker(e.latlng).addTo(map);

  latUser = e.latlng.lat;
  longUser = e.latlng.lng;
  getlocation();
});

// Recoge las coordenadas del punto indicado
const getlocation = () => {
  console.log(navigator.geolocation);
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(showLocation, showError);
  } else {
    alert("No location encontrada");
  }
};

const showError = (error) => {
  console.log("Error");
};

// Conversion de coordenadas a Ciudad y persistencia en localStorage
const showLocation = async (position) => {
  let lat = latUser;
  let long = longUser;

  console.log(lat, long);

  let response = await fetch(
    `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${long}&format=json`
  );
  let data = await response.json();

  console.log(data.address.city);
  localStorage.setItem('currentLoc', data.address.city);
  window.close();
};