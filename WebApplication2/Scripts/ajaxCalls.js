﻿

function handleClick(object) {

    if (object.value === "OneWay") {
        $("#arrivaDateVisibility").hide();
       // $("#arrivalPlaceVisibility").hide();
    }
    //if(object.value === "RoundTrip") 
    else{
       $("#arrivaDateVisibility").show();
       $("#arrivalPlaceVisibility").show();
    }
}
$(function () {
    $('#searchInfo').on('click', function () {
        //Get all the data passed by the user
        var departure = $('#departurePlace').val();
        var arrivalInfo = $('#arrivalPlace').val();
        var departureDate = $('#departureDate').val();
        var arrivalDate = $('#arrivalDate').val();
        var totalNumAdults = $('#numAdults').val();
        var totalNumChildren = $('#numChildren').val();
        var totalNumInfants = $('#numInfants').val();
        var radioValue = $('input[name=flight-options]:checked').val();

        if (radioValue === "OneWay") {

            var searchDetailsObjects = {
                "Departure": departure,
                "Arrival": arrivalInfo,
                "DepartureDate": departureDate,
                "ArrivalDate": arrivalDate,
                "OneWay": radioValue,
                "TotalAdults": totalNumAdults,
                "TotalChildren": totalNumChildren,
                "TotalInfants": totalNumInfants

            };
            $.ajax({
                type: "POST",
                url: '/api/Values/SearchFlightDetails',
                data: JSON.stringify(searchDetailsObjects),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: successFunc,
                error: errorFunc
            });

        }
        else {
            var searchDetailsObjectsRoundTrip = {
                "Departure": departure,
                "Arrival": arrivalInfo,
                "DepartureDate": departureDate,
                "ArrivalDate": arrivalDate,
                "RoundTrip": radioValue,               
                "TotalAdults": totalNumAdults,
                "TotalChildren": totalNumChildren,
                "TotalInfants": totalNumInfants

            };
            $.ajax({
                type: "POST",
                url: '/api/Values/SearchFlightDetails',
                data: JSON.stringify(searchDetailsObjectsRoundTrip),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: successFunc,
                error: errorFunc
            });

        }
             

        function successFunc(data, status) {
            alert(data);
        }

        function errorFunc(ex) {
            alert('error: ', ex);
        }
       
          
          
    });


   
});