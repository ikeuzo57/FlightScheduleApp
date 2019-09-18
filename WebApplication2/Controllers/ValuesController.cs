using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        // [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult SearchFlightDetails([FromBody] SearchFlightDetails searchFlightDetailsObject)
        {
            List<FlightResult> flightResultList = new List<FlightResult>();

            var departurCity = searchFlightDetailsObject.Departure;
            var arrivalCity = searchFlightDetailsObject.Arrival;
            var arrivalCityCode = string.Empty;
            // Retrieve Departure City Code
            var departureCityCode = RunAPI.GetFlightCityCode(RunAPI.CitiesTypeAheadEndPoint, departurCity); //Kansas City, MO, USA

            if (!string.IsNullOrEmpty(arrivalCity))
            {
                arrivalCityCode = RunAPI.GetFlightCityCode(RunAPI.CitiesTypeAheadEndPoint, arrivalCity);
            }

            searchFlightDetailsObject.Departure = departureCityCode;
            searchFlightDetailsObject.Arrival = arrivalCityCode;
           // searchFlightDetailsObject.DepartureDate = Convert.ToDateTime(searchFlightDetailsObject.DepartureDate).ToString("MM/dd/yyyy");

            //Get searchFlightJsonObject
            string searchFlightJsonObject =
                RunAPI.FormatFlightSearchJsonObject(searchFlightDetailsObject.Departure,
                                                    searchFlightDetailsObject.Arrival,
                                                    searchFlightDetailsObject.DepartureDate,
                                                    numberOfAdult: Convert.ToInt32(searchFlightDetailsObject.TotalAdults),
                                                    numberOfChild: Convert.ToInt32(searchFlightDetailsObject.TotalChildren));

            //Search flights - Call Flight search endpoint
            var searchFlightsResult = RunAPI.PostSearchFlightAsync(RunAPI.SearchFlightEndPoint, searchFlightJsonObject).Result;

            //Deserialize flight results data
            dynamic deserializedSearchFlightList = JsonConvert.DeserializeObject(searchFlightsResult);
            var resultItineraries = deserializedSearchFlightList.body.data.itineraries;
            foreach (var itinerary in resultItineraries)
            {
                var flightResult = new FlightResult();

                foreach (var originDestination in itinerary.origin_destinations)
                {
                    //Elapsed Time
                    flightResult.ElapsedTime = Convert.ToString(originDestination.elapsed_time);

                    //Flight segments
                    foreach (var segment in originDestination.segments)
                    {
                        flightResult.FlightSegmentList.Add(new FlightSegment
                        {
                            DepartureDate = Convert.ToString(segment.departure.date),
                            DepartureTime = Convert.ToString(segment.departure.time),
                            DepartureAirport = Convert.ToString(segment.departure.airport.name),
                            DepartureTerminal = Convert.ToString(segment.departure.airport.terminal),
                            ArrivalDate = Convert.ToString(segment.arrival.date),
                            ArrivalTime = Convert.ToString(segment.arrival.time),
                            ArrivalAirport = Convert.ToString(segment.arrival.airport.name),
                            ArrivalTerminal = Convert.ToString(segment.arrival.airport.terminal),
                            FlightNumber = Convert.ToString(segment.flight_number),
                            FlightDuration = Convert.ToString(segment.flight_duration),
                            OperatingAirline = Convert.ToString(segment.operating_airline.name),
                            MarketingAirline = Convert.ToString(segment.marketing_airline.name),
                            BaggageAllowance = Convert.ToString(segment.baggage[0].baggage.quantity) + Convert.ToString(segment.baggage[0].baggage.unit)
                        });
                    }

                    //Cabin - Economy, Business, First Class etc
                    flightResult.Cabin = Convert.ToString(itinerary.cabin.name);
                    //Currency
                    flightResult.PriceCurrency = Convert.ToString(itinerary.pricing.provider.currency.code);
                    //Base Fare
                    flightResult.BaseFarePrice = Convert.ToString(itinerary.pricing.provider.base_fare);
                    //Total Fare
                    flightResult.TotalFarePrice = Convert.ToString(itinerary.pricing.provider.total_fare);

                    foreach (var fareBreakDownItem in itinerary.pricing.provider.fare_break_down)
                    {
                        flightResult.FlightPriceList.Add(new FlightPrice
                        {
                            PassengerCode = Convert.ToString(fareBreakDownItem.passenger.code),
                            PassengerQty = Convert.ToString(fareBreakDownItem.passenger.quantity),
                            TotalPassengerFarePrice = Convert.ToString(fareBreakDownItem.provider_fare.total),
                            PassengerFareTax = Convert.ToString(fareBreakDownItem.provider_fare.taxes[0]),
                        });
                    }
                }

                flightResultList.Add(flightResult);
            }

            return Json(flightResultList); //flightResultList contains extracted flight info
        }

        // PUT api/values/5
        public void Put([FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
