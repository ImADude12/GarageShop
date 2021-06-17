

$(window).on('load', function () {
    getMap();
});



function getMap() {
        var branches = [{
            location:
                { lat: 31.971823288843936, lon: 34.803743362426765 },
            name: "סניף ראשון לציון"
        }
            , {
            location:
                { lat: 31.571855588843936, lon: 34.803743362426765 },
            name: "סניף קריית גת"
        }
            , {
            location:
                { lat: 32.571855588843936, lon: 35.103743362426765 },
            name: "סניף חיפה"
        }
        ]
    try {
        var map = new Microsoft.Maps.Map('.map-container', {
            credentials: 'BING MAPS KEY',
            center: new Microsoft.Maps.Location(31.971823288843936, 34.803743362426765),
            mapTypeId: Microsoft.Maps.MapTypeId.road,
            zoom: 8
        });
        branches.forEach(branch => {
            createPin(branch.location.lat, branch.location.lon, map, branch.name);
        });
    }
    catch (Ex) {
       alert("Failed try to get map again")
        setTimeout(() => getMap(), 1000)
        }
}

function createPin(latitude, longitude, map, name) {
    var createLocation = {
        latitude,
        longitude
    }
    var pin = new Microsoft.Maps.Pushpin(createLocation, {
        icon: 'https://i.ibb.co/8cZhwXL/pin.png',
        anchor: new Microsoft.Maps.Point(12, 32),
        title: name
    });
    map.entities.push(pin);
}