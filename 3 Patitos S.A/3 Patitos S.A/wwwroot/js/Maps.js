// Initialize and add the map
function initMap() {
    // The location of Uluru
    const uluru = { lat: 10.00585909857488, lng: -84.15327973991106 };
    // The map, centered at Uluru
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 15,
        center: uluru,
    });
    // The marker, positioned at Uluru
    const marker = new google.maps.Marker({
        position: uluru,
        map: map,
    });
}

window.initMap = initMap;