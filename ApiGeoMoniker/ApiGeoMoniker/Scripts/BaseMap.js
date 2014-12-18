var MapControl;
var LastMarker;//=new google.maps.Marker();
var Terminals = [];
$(function () {
    LastMarker = new google.maps.Marker();
    $("#AddTerminal").dialog(
        {
            autoOpen: false,
            buttons:
                {
                    "Guardar":function()
                    {
                        $("#saveForm").submit();
                    },
                    Cancel: function () {
                        $("#saveForm > input[type=text]").attr("value", "");
                        $(this).dialog("close");
                    }
                },
            show:
                {
                    effect: "blind",
                    duration:1000
                },
            height: 340,
            width: 250,
            title: "Alta de Terminal"
        });

    $("#confirmupdate").dialog(
        {
            autoOpen: false,
            buttons:
                {
                    "Aceptar": function () {
                        var stringJson = JSON.stringify({ Id: LastMarker.attr("id"), Description: LastMarker.attr("title"), Lat: LastMarker[0].getPosition().lat(), Lng: LastMarker[0].getPosition().lng() })
                        var ejemplo = stringJson;
                        $.ajax({
                            url: "http://" + urlApi + "/api/route/1/point",
                            type: "PUT",
                            dataType: "json",
                            data: stringJson,
                            contentType: "application/json",
                            success: function (response) {
                                $("#confirmupdate").dialog("close");
                            },
                            error: function (a, b, c) {
                                var errorResponse = $.parseJSON(a.responseText);
                                alert(c + "\nMensaje:" + errorResponse.Message);
                            }
                        });
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                },
            show:
                {
                    effect: "blind",
                    duration: 1000
                },
            height: 340,
            width: 250,
            title: "Cambiar Ubicación"
        });
    
    $("#saveForm").on("submit",function(event)
    {
        $.ajax({
            url: "http://" + urlApi + "/api/route/1/point",
            type: "POST",
            dataType: "json",
            data: TransformObject($("#saveForm").serializeArray()),
            contentType: "application/json",
            success: function (response) {
                $("#AddTerminal").dialog("close");
                $("#saveForm > input[type=text]").attr("value", "");
                CleanMap(1);
            },
            error: function (a, b, c) {
                var errorResponse = $.parseJSON(a.responseText);
                alert(c + "\nMensaje:" + errorResponse.Message);
            }
        });

        event.preventDefault();
    });

    //$("#AddTerminal").dialog("hide");
    MapControl = MapObj();
    var qs = queryParameters();
    var zoom = qs.zoom;
    zoom = (zoom != null) ? parseInt(zoom) : 17;
    if (qs.lat != null && qs.lng != null)
        MapControl.Init(qs.lat, qs.lng, zoom);
    MapControl.LoadPoints(1);
    

    
});

function CleanMap(route) {
    $.each(Terminals, function (index, terminal) {
        terminal.setMap();
    });
    Terminals = [];
    MapControl.LoadPoints(route);
}

function TransformObject(parameters) {
    var result = "{";
    $.each(parameters, function (index, value) {
        result += value.name + ":" + ((isNaN(value.value)) ? "'" + value.value + "'" : value.value) + ",";
    });
    result = result.substring(0, result.length - 1) + "}";
    return result;
};

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
    var LastPosition;

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
            $("#AddTerminal").dialog({tipo:1});
            $("#AddTerminal").dialog("open");
            $("#Lat").val(position.latLng.lat());
            $("#Lng").val(position.latLng.lng());
        });
    };


    var addMarker = function (posx, posy,titulo,id) {
        var marker = new google.maps.Marker(
            {
                position:new google.maps.LatLng(posx,posy),
                map:map,
                title: titulo,
                id:id,
                draggable:true,
                icon:{
                    path:google.maps.SymbolPath.CIRCLE,
                    scale:4,
                    fillColor:'blue',
                    fillOpacity:0.6,
                    strokeColor:'red',
                    strokeWeigth:0
                }
            });

        var infowindow = new google.maps.InfoWindow();

        google.maps.event.addListener(marker, "click", function (LatLng) {

            var marker = $(this);
            var content = '<div id="contentInfo">' +
                '<div>' + $(this).attr("title") + '</div>' +
                '<div><string>Dirección:</strong></div>' +
                '</div>';
            infowindow.setContent(content);
            infowindow.open(map,marker[0]);
        });

        google.maps.event.addListener(marker, "dragend", function (LatLng) {
            LastMarker = $(this);
            confirmUpdateUbication();
            
        });

        google.maps.event.addListener(marker, "dragstart", function (LatLng) {
            LastPosition = LatLng.latLng;
        });

        Terminals.push(marker);
    };

    var loadTerminal = function (idRoute) {
        var bound = new google.maps.LatLngBounds();
        $.ajax({
            url: "http://"+urlApi+"/api/route/"+idRoute+"/point",
            type: "GET",
            dataType: "json",
            success: function (response) {
                $.each(response, function (index,marker) {
                    addMarker(marker.Lat, marker.Lng, marker.Description,marker.Id);
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

function confirmUpdateUbication() {
    $("#confirmupdate").dialog({ title: "Moviendo Ubicación de Terminal" });
    $("#confirmupdate > p").text("¿Deseas Mover la Ubicación de " + LastMarker.attr("title") + "?");
    $("#confirmupdate").dialog("open");
}