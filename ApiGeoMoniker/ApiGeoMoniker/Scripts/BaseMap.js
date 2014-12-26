var MapControl;
var LastMarker;//=new google.maps.Marker();
var Terminals = [];
var Oneness = [];
var LastlatLng
var Areas = [];
var editMode = false;
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
            height: 430,
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
                        $.each(Areas, function (index, obj) {
                            if(obj.id==LastMarker.attr("id"))
                                obj.setCenter(LastMarker[0].getPosition());
                        });

                        $.ajax({
                            url: "http://" + urlApi + "/api/route/1/point/" + LastMarker.attr("id"),
                            type: "GET",
                            dataType: "json",

                            contentType: "application/json",
                            success: function (response) {
                                stringJson = JSON.stringify({ Id: response.Id, Description: response.Description, Lat: LastMarker[0].getPosition().lat(), Lng: LastMarker[0].getPosition().lng(), IsStart: response.IsStart, IsEnd: response.isEnd, Order: response.Order })
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

    if (qs.edit != null && qs.edit == "true")
        editMode = true;
    MapControl.LoadPoints(1);

    LastlatLng = new google.maps.LatLng();
    getUnits(1, 1);
    window.setInterval(function () {
        UpdateUnits(1, 1);
    }, 20000);
    

    
});

function CleanMap(route) {
    $.each(Terminals, function (index, terminal) {
        terminal.setMap();
    });
    Terminals = [];
    MapControl.LoadPoints(route);
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
            $("#saveForm > input[type=text]").attr("value", "");
            $("#saveForm > fieldset > input[type=checkbox]").attr("checked", false);
            $("#saveForm > input[type=number]").attr("value", "");
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
                draggable:editMode,
                icon:{
                    path:google.maps.SymbolPath.CIRCLE,
                    scale:3,
                    fillColor:'green',
                    fillOpacity:0.8,
                    strokeColor:'orange',
                    strokeWeigth:0
                }
            });

        var TerminalAreaParams=
            {
                strokeColor: "red",
                strokeOpacity: "red",
                strokeWeight: 2,
                fillColor: "blue",
                fillOpacity: 0.2,
                map: map,
                center: marker.getPosition(),
                radius: 10,
                id:id
            }

        var area = new google.maps.Circle(TerminalAreaParams);

        Areas.push(area);

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
        var NumWayPoints = parseInt((Terminals.length-2) / 4);

        for (i = 1; i < Terminals.length - 1; i=i+NumWayPoints) {
            arrayPoints.push(
                {
                    location: Terminals[i].getPosition(),
                    stopover:false
                });
        }
        
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

    var addOneness = function (markerobj) {
        var marker = new google.maps.Marker(
            {
                position: new google.maps.LatLng(markerobj.Lat, markerobj.Lng),
                map: map,
                title: markerobj.Name,
                id: markerobj.Id,
                draggable: false,
                icon: {
                    path: google.maps.SymbolPath.CIRCLE,
                    fillColor: "black",
                    scale:5
                }
            });

        Oneness.push(marker);
    };

    return {
        Init: init,
        LoadPoints: loadTerminal,
        TraceRoute: TraceRoute,
        AddOneness:addOneness
    }
}

function confirmUpdateUbication() {
    $("#confirmupdate").dialog({ title: "Moviendo Ubicación de Terminal" });
    $("#confirmupdate > p").text("¿Deseas Mover la Ubicación de " + LastMarker.attr("title") + "?");
    $("#confirmupdate").dialog("open");
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

function getUnits(companyId,UserId) {
    $.ajax({
        url: "http://" + urlApi + "/api/company/" + companyId + "/user/"+UserId+"/oneness/",
        type: "GET",
        dataType: "json",
        success: function (response) {

            $.each(response, function (index, marker) {
                MapControl.AddOneness(marker);
            });
        },
        error: function (a, b, c) {
            alert(z);
        }
    });
}

function UpdateUnits(companyId, UserId) {
    $.ajax({
        url: "http://" + urlApi + "/api/company/" + companyId + "/user/" + UserId + "/oneness/",
        type: "GET",
        dataType: "json",
        success: function (response) {

            $.each(response, function (index, marker) {
                $.each(Oneness, function (index, mark) {
                    if (marker.Id == mark.id)
                        mark.setPosition(new google.maps.LatLng(marker.Lat, marker.Lng));
                });
            });
        },
        error: function (a, b, c) {
            alert(z);
        }
    });

}