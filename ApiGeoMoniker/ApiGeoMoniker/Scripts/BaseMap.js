var MapControl;
var LastMarker;//=new google.maps.Marker();
var Terminals = [];
var LastlatLng
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
                        $("#saveForm > fieldset > input[type=checkbox]").attr("checked", false);
                        $("#saveForm > input[type=number]").attr("value", "");
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
                        $.ajax({
                            url: "http://" + urlApi + "/api/route/1/point",
                            type: "PUT",
                            dataType: "json",
                            data: stringJson,
                            contentType: "application/json",
                            success: function (response) {
                                $("#confirmupdate").dialog("close");
                                CleanMap(1);
                            },
                            error: function (a, b, c) {
                                var errorResponse = $.parseJSON(a.responseText);
                                alert(c + "\nMensaje:" + errorResponse.Message);
                            }
                        });
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        LastMarker[0].setPosition(LastlatLng);
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
        var json = TransformObject($("#saveForm").serializeArray());
        if ($("#Id").val() == 0) {
            $.ajax({
                url: "http://" + urlApi + "/api/route/1/point",
                type: "POST",
                dataType: "json",
                data: json,
                contentType: "application/json",
                success: function (response) {
                    $("#AddTerminal").dialog("close");
                    $("#saveForm > input[type=text]").attr("value", "");
                    CleanMap(1);
                },
                error: function (a, b, c) {
                    var errorResponse = $.parseJSON(a.responseText);
                    alert(c + "\nMensaje:" + errorResponse.Message);
                    $("#saveForm > input[type=text]").attr("value", "");
                    $("#saveForm > fieldset > input[type=checkbox]").attr("checked", false);
                    $("#saveForm > input[type=number]").attr("value", "");
                    ("#AddTerminal").dialog("close");
                }
            });
        }
        else {

            $.ajax({
                url: "http://" + urlApi + "/api/route/1/point",
                type: "PUT",
                dataType: "json",
                data: json,
                contentType: "application/json",
                success: function (response) {
                    $("#AddTerminal").dialog("close");
                    $("#saveForm > input[type=text]").attr("value", "");
                    $("#saveForm > input[type=number]").attr("value", "");
                    $("#saveForm > fieldset > input[type=checkbox]").attr("checked", false);
                    CleanMap(1);
                },
                error: function (a, b, c) {
                    var errorResponse = $.parseJSON(a.responseText);
                    alert(c + "\nMensaje:" + errorResponse.Message);
                }
            });
        }

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

    LastlatLng = new google.maps.LatLng();
    

    
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
        value.value = (value.value == "on") ? 1 : value.value;
        result += value.name + ":" + ((isNaN(value.value)) ?
            "'" + value.value + "'" :
            value.value) + ",";
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
    var directionDisplay;

    var init = function (posx, posy, zoom) {
        var mapOptions = {
            zoom: zoom,
            center: new google.maps.LatLng(posx, posy),
            mapTypeControl: false,
            panControl: false
        }
        map = new google.maps.Map(document.getElementById("map"), mapOptions);

        directionDisplay = new google.maps.DirectionsRenderer();
        directionDisplay.setMap(map);

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

        google.maps.event.addListener(marker, "dblclick", function (LatLng) {
            $.ajax({
                url: "http://" + urlApi + "/api/route/1/point/"+$(this).attr("id"),
                type: "GET",
                dataType: "json",
                
                contentType: "application/json",
                success: function (response) {
                    $("#Id").val(response.Id)
                    $("#Description").val(response.Description);
                    $("#Lat").val(response.Lat);
                    $("#Lng").val(response.Lng);
                    $("#IsStart").attr("checked", response.IsStart);
                    $("#IsEnd").attr("checked", response.isEnd);
                    $("#Order").val(response.Order);
                    $("#AddTerminal").dialog("open");

                },
                error: function (a, b, c) {
                    var errorResponse = $.parseJSON(a.responseText);
                    alert(c + "\nMensaje:" + errorResponse.Message);
                }
            });
        });

        google.maps.event.addListener(marker, "dragend", function (LatLng) {
            LastMarker = $(this);
            confirmUpdateUbication();
            
        });

        google.maps.event.addListener(marker, "dragstart", function (LatLng) {
            LastlatLng = LatLng.latLng;
        });

        Terminals.push(marker);
    };

    var loadTerminal = function (idRoute) {
        var bound = new google.maps.LatLngBounds();
        var start=null;
        var end=null;
        $.ajax({
            url: "http://"+urlApi+"/api/route/"+idRoute+"/point",
            type: "GET",
            dataType: "json",
            success: function (response) {

                $.each(response, function (index,marker) {
                    addMarker(marker.Lat, marker.Lng, marker.Description,marker.Id);
                    bound.extend(new google.maps.LatLng(marker.Lat, marker.Lng));
                    if (marker.IsStart)
                        start = new google.maps.LatLng(marker.Lat, marker.Lng);
                    if(marker.isEnd)
                        end = new google.maps.LatLng(marker.Lat, marker.Lng);
                });
                if (start != null || end != null)
                    TraceRoute(start, end);
                else
                    alert("Falta Establecer el Inicio y/o final de la ruta");
                map.fitBounds(bound);
                
            },
            error: function (a, b, c) {
                alert(z);
            }
        });
    };

    var TraceRoute = function (origin, destination) {
        var arrayPoints = [];
        var routes = [];
        var NumWayPoints = parseInt(Terminals.length / 2);

        
            arrayPoints.push(
                {
                    location: Terminals[2].getPosition(),
                    stopover:true
                });

            arrayPoints.push(
                    {
                        location: Terminals[(Terminals.length - 1)].getPosition(),
                        stopover: true
                    });
        
            var request =
            {
                origin: origin,
                destination: destination,
                travelMode: google.maps.TravelMode.DRIVING,
                waypoints:arrayPoints
            };

            var directionService = new google.maps.DirectionsService();
            directionService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionDisplay.setDirections(response);
                }
                else {
                    alert(status);
                }
            });
        
       
       

        


    };



    return {
        Init: init,
        LoadPoints: loadTerminal,
        TraceRoute:TraceRoute
    }
}

function confirmUpdateUbication() {
    $("#confirmupdate").dialog({ title: "Moviendo Ubicación de Terminal" });
    $("#confirmupdate > p").text("¿Deseas Mover la Ubicación de " + LastMarker.attr("title") + "?");
    $("#confirmupdate").dialog("open");
}