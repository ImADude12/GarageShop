

$(window).on('load', function () {
    getMap();
});



function getMap() {
     
    try {
       
       $.ajax({
            type: "GET",
            url: `branches/All`,
            success: initialMap,
        })
        
        function initialMap(branches) {
            const lat = branches.length > 0 ? branches[0].latitude : 31.97102;
            const lon = branches.length > 0 ? branches[0].longitude : 34.78939;
        var map = new Microsoft.Maps.Map('.map-container', {
            credentials: 'AtQ5syZg_44nIy3Vc1B2mRtzIlUMpPHTlAXyywJbXIWVWWgqfIjmDJ0fUyeC-sSo',
            center: new Microsoft.Maps.Location( lat,  lon),
            mapTypeId: Microsoft.Maps.MapTypeId.road,
            zoom: 8
        });

            if (branches.length > 0) {
            var str = '|';
        branches.forEach(branch => {
            createPin(branch.latitude, branch.longitude, map, branch.name);
            str += ` ${branch.name} ${branch.address} ${branch.phoneNumber} |`
        });
            }
            else {
            str = "no branches in db yet.."
            }
        $("#branches").text(str);
        }
    }
    catch (Ex) {
        alert("Failed try to get map again")
        console.log(Ex)
        setTimeout(() => getMap(), 10000)
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