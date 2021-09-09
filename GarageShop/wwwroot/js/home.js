$(function () {
    getCoordintes()
});
function getCoordintes() {
    var options = {
        enableHighAccuracy: true,
        timeout: 5000,
        maximumAge: 0
    };

    function success(pos) {
        var crd = pos.coords;
        var lat = getFlooredFixed(crd.latitude ,2).toString();
        var lng = getFlooredFixed(crd.longitude, 2).toString();

        console.log(`Latitude: ${lat}, Longitude: ${lng}`);
        getWeather(lat,lng);
        return;

    }

    function error(err) {
        console.warn(`ERROR(${err.code}): ${err.message}`);
    }

    navigator.geolocation.getCurrentPosition(success, error, options);
}
function getFlooredFixed(v, d) {
    return (Math.floor(v * Math.pow(10, d)) / Math.pow(10, d)).toFixed(d);
}

function getWeather(lat,lng) {
    var ajaxCall = function () {
        $.ajax({
            url: `https://api.weatherapi.com/v1/current.json?key=dc5c317d52cd480dbd3185526211506&q=${lat},${lng}&aqi=yes `,
            success: setWeather,
        })
    }();
    setInterval(ajaxCall,60 * 1000 * 60 )
   
}

function setWeather(res) {
    var weather = JSON.parse(JSON.stringify(res));
    var date = weather.location.localtime.split(" ")
    var currWeather = weather.current
    $(".date").html(date[0]);
    $(".location").html(weather.location.name + " , " + weather.location.country);
    $(".condition-icon").attr("src", currWeather.condition.icon);
    $(".temp").html(currWeather.temp_c);
    $(".condition").html(currWeather.condition.text);
}