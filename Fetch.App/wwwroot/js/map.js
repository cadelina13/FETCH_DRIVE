var directionsRenderer = new google.maps.DirectionsRenderer();
var directionsService = new google.maps.DirectionsService();
var marker = null;
function initMap(mapContainer) {
    var map = new google.maps.Map(mapContainer, {
        center: { lat: 37.7749, lng: -122.4194 },
        zoom: 8
    });
    DotNet.invokeMethodAsync('Fetch.App', 'MapInitialized');
}

function getRoute() {

    var source_latitude = localStorage.getItem('source_latitude');
    var source_longitude = localStorage.getItem('source_longitude');
    var destination_latitude = localStorage.getItem('destination_latitude');
    var destination_longitude = localStorage.getItem('destination_longitude');
    var start = new google.maps.LatLng(source_latitude, source_longitude);
    var end = new google.maps.LatLng(destination_latitude, destination_longitude);
    var request = {
        origin: start,
        destination: end,
        travelMode: 'DRIVING'
    };
    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            alert(JSON.stringify(result));
            directionsRenderer.setDirections(result);
            marker.setMap(null);
        }
    });
}

function setDestinationMapLocation(mapContainer, _lat, _lng) {
    directionsService = new google.maps.DirectionsService();
    var map = new google.maps.Map(mapContainer, {
        center: { lat: _lat, lng: _lng },
        zoom: 13
    });
    const myLatLng = { lat: _lat, lng: _lng };

    marker = new google.maps.Marker({
        position: myLatLng,
        map,
        title: "Destination",
    });
    google.maps.event.addListener(map, 'drag', function () {
        marker.setPosition(map.getCenter());
    });

    google.maps.event.addListener(map, 'dragend', function () {
        marker.setPosition(map.getCenter());
        saveDestinationLocation(marker.getPosition());
    });
    google.maps.event.addListener(map, 'zoom_changed', function () {
        marker.setPosition(map.getCenter());
        saveDestinationLocation(marker.getPosition());
    });
    google.maps.event.addListener(map, 'mousemove', function () {
        marker.setPosition(map.getCenter());
    });

    directionsRenderer.setMap(map);
}

function getLocation(mapContainer) {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var _lat = position.coords.latitude;
            var _lng = position.coords.longitude;
            const myLatLng = { lat: _lat, lng: _lng };

            var map = new google.maps.Map(mapContainer, {
                center: myLatLng,
                zoom: 13
            });
            marker = new google.maps.Marker({
                position: myLatLng,
                map,
                title: "You",
            });

            google.maps.event.addListener(map, 'drag', function () {
                marker.setPosition(map.getCenter());
            });

            google.maps.event.addListener(map, 'dragend', function () {
                marker.setPosition(map.getCenter());
                saveSourceLocation(marker.getPosition());
            });
            google.maps.event.addListener(map, 'zoom_changed', function () {
                marker.setPosition(map.getCenter());
                saveSourceLocation(marker.getPosition());
            });
            google.maps.event.addListener(map, 'mousemove', function () {
                marker.setPosition(map.getCenter());
            });
            var geoloccontrol = new klokantech.GeolocationControl(map, 13);
        });
    } else {
        alert("Geolocation is not supported by this browser.");
    }
}

function saveSourceLocation(location) {
    localStorage.setItem('source_latitude', location.lat());
    localStorage.setItem('source_longitude', location.lng());
}
function saveDestinationLocation(location) {
    localStorage.setItem('destination_latitude', location.lat());
    localStorage.setItem('destination_longitude', location.lng());
}


