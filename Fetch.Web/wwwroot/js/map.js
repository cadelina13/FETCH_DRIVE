var directionsRenderer = new google.maps.DirectionsRenderer();
var directionsService = new google.maps.DirectionsService();
var marker = null;
function initMap(mapContainer) {
    var map = new google.maps.Map(mapContainer, {
        center: { lat: 37.7749, lng: -122.4194 },
        zoom: 8
    });
    //DotNet.invokeMethodAsync('Fetch.App', 'MapInitialized');
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
            directionsRenderer.setDirections(result);
            marker.setMap(null);
            var strArray = ["", ""];
            for (var i = 0; i < result['routes'].length; i++) {
                strArray[0] = result['routes'][i].legs[0].start_address;
                strArray[1] = result['routes'][i].legs[0].end_address;
            }
            DotNet.invokeMethodAsync('Fetch.Web', 'DestinationResultMethodAsync', strArray);
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
    marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png');

    google.maps.event.addListener(map, 'drag', function () {
        marker.setPosition(map.getCenter());
        marker.setMap(map);
    });

    google.maps.event.addListener(map, 'dragend', function () {
        marker.setPosition(map.getCenter());
        saveDestinationLocation(marker.getPosition());
    });
    google.maps.event.addListener(map, 'zoom_changed', function () {
        marker.setPosition(map.getCenter());
        saveDestinationLocation(marker.getPosition());
        marker.setMap(map);

    });
    google.maps.event.addListener(map, 'mousemove', function () {
        marker.setPosition(map.getCenter());
    });

    directionsRenderer.setMap(map);
}

function getLocation(mapContainer, searchBoxContainer) {
    directionsRenderer = new google.maps.DirectionsRenderer();
    directionsService = new google.maps.DirectionsService();
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var _lat = position.coords.latitude;
            var _lng = position.coords.longitude;
            const myLatLng = { lat: _lat, lng: _lng };

            var map = new google.maps.Map(mapContainer, {
                center: myLatLng,
                zoom: 13,
                scaleControl: false,
                zoomControl: false
            });
            initAutocomplete(map, searchBoxContainer);
            marker = new google.maps.Marker({
                position: myLatLng,
                map,
                title: "You",
            });
            marker.setIcon('http://maps.google.com/mapfiles/ms/icons/green-dot.png')

            google.maps.event.addListener(map, 'drag', function () {
                marker.setPosition(map.getCenter());
                marker.setMap(map);

            });

            google.maps.event.addListener(map, 'dragend', function () {
                marker.setPosition(map.getCenter());
                saveSourceLocation(marker.getPosition());
            });
            google.maps.event.addListener(map, 'zoom_changed', function () {
                marker.setPosition(map.getCenter());
                saveSourceLocation(marker.getPosition());
                marker.setMap(map);

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




// This example adds a search box to a map, using the Google Place Autocomplete
// feature. People can enter geographical searches. The search box will return a
// pick list containing a mix of places and predicted search terms.
// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">
function initAutocomplete(map, searchBoxContainer) {
    const searchBox = new google.maps.places.SearchBox(searchBoxContainer);

    map.controls[google.maps.ControlPosition.TOP_LEFT].push(searchBox);
    // Bias the SearchBox results towards current map's viewport.
    map.addListener("bounds_changed", () => {
        searchBox.setBounds(map.getBounds());
    });

    let markers = [];

    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener("places_changed", () => {
        const places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach((marker) => {
            marker.setMap(null);
        });
        markers = [];

        // For each place, get the icon, name and location.
        const bounds = new google.maps.LatLngBounds();

        places.forEach((place) => {
            if (!place.geometry || !place.geometry.location) {
                console.log("Returned place contains no geometry");
                return;
            }

            const icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25),
            };

            // Create a marker for each place.
            markers.push(
                new google.maps.Marker({
                    map,
                    icon,
                    title: place.name,
                    position: place.geometry.location,
                })
            );
            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
    });
}

