

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
    $('#datetimepicker6').datetimepicker({
        format: 'MM/DD/YYYY'
    });
    $('#datetimepicker7').datetimepicker({
        format: 'MM/DD/YYYY',
        useCurrent: false //Important! See issue #1075
    });
    $("#datetimepicker6").on("dp.change", function (e) {
        $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
    });
    $("#datetimepicker7").on("dp.change", function (e) {
        $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
    });
});


function ReturnedValues() {
    var myVar = localStorage['myKey'] || 'defaultValue';
    $.each(myVar, function (index, val) {
        console.log("Stored Data");
        console.log(val);
    });

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
                success: function (data) {
                    // console.log(data);
                    $.each(JSON.parse(data), function (index, val) {
                        console.log("List of Object***");
                        console.log(val);
                    });
                },

                error: function (ex) {
                    console.log('error: ', ex);
                    alert('error: ', ex);
                }
            });

        } else if (radioValue === "RoundTrip") {  // Implementation for RoundTrip


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
                success: function (data, textStatus, jqXHR) {
                    var flightResultTable = $('#tblFlightResult tbody');
                    $("#flightResultDiv").show();
                   
                    $.each(data, function (index, val) {
                        var departureDate = val.FlightSegmentList[0].DepartureDate;
                        var departureTime = val.FlightSegmentList[0].DepartureTime;
                        var arrivalDate = val.FlightSegmentList[0].ArrivalDate;
                        var arrivalTime = val.FlightSegmentList[0].ArrivalTime;
                        var airLine = val.FlightSegmentList[0].OperatingAirline;
                        var price = val.PriceCurrency +' '+ val.TotalFarePrice;                     
                        flightResultTable.append('<tr><td>' + departureDate + '</td><td>' + departureTime + '</td><td>' +
                            arrivalDate + '</td><td>' + arrivalTime + '</td><td>' + airLine + '</td><td>' + price + '</td></tr>');
                                                    
                       // console.log(val);
                        
                    });
                    localStorage['myKey'] = data;
                   // window.location.href = "http://localhost:54342/Home/FlightResultData";
                    
                },

                error: function (ex) {
                    console.log('error: ', ex.responseJSON);
                    
                }
            });


        }
       
        departure = "";
        arrivalInfo = "";
        departureDate = "";
       
    });


   
});

$(document).ready(function () {

    $("#flightResultDiv").hide();
  

});