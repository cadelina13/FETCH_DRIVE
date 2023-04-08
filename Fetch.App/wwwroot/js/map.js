function initMap(mapContainer) {
    var map = new google.maps.Map(mapContainer, {
        center: { lat: 37.7749, lng: -122.4194 },
        zoom: 8
    });
    DotNet.invokeMethodAsync('YourAppNameHere', 'MapInitialized');
}
