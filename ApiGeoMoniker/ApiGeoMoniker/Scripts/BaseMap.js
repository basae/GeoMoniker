var MapControl;
$(function () {
    MapControl = MapObj();
    var qs = queryParameters();
    var zoom = qs.zoom;
    zoom = (zoom != null) ? parseInt(zoom) : 17;
    if (qs.lat != null && qs.lng != null)
        MapControl.Init(qs.lat, qs.lng, zoom);
    MapControl.LoadPoints(1);
    

    
});

function queryParameters() {
    var result = {};

    var params = window.location.search.split(/\?|\&/);

    params.forEach(function (it) {
        if (it) {
            var param = it.split("=");
            result[param[0]] = param[1];
        }
    });

    return result;
}

var MapObj = function () {
    var map;

    var init = function (posx, posy, zoom) {
        var mapOptions = {
            zoom: zoom,
            center: new google.maps.LatLng(posx, posy),
            mapTypeControl: false,
            panControl: false
        }
        map = new google.maps.Map(document.getElementById("map"), mapOptions);

        //addMarker(posx, posy,'');

        google.maps.event.addListener(map, "rightclick", function (position) {

        });
    };


    var addMarker = function (posx, posy,titulo) {
        var marker = new google.maps.Marker(
            {
                position:new google.maps.LatLng(posx,posy),
                map:map,
                title:titulo
            });
    };

    var loadTerminal = function (idRoute) {
        var bound = new google.maps.LatLngBounds();
        $.ajax({
            url: "http://"+urlApi+"/api/route/"+idRoute+"/point",
            type: "GET",
            dataType: "json",
            success: function (response) {
                $.each(response, function (index,marker) {
                    addMarker(marker.Lat, marker.Lng, marker.Description);
                    bound.extend(new google.maps.LatLng(marker.Lat, marker.Lng));
                });
                map.fitBounds(bound);
                
            },
            error: function (a, b, c) {
                alert(z);
            }
        });
    };



    return {
        Init: init,
        LoadPoints:loadTerminal
    }
}