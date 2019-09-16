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
            searchFlightDetailsObject.DepartureDate = Convert.ToDateTime(searchFlightDetailsObject.DepartureDate).ToString("MM/dd/yyyy");

            //Get searchFlightJsonObject
            string searchFlightJsonObject =
                RunAPI.FormatFlightSearchJsonObject(searchFlightDetailsObject.Departure,
                                                    searchFlightDetailsObject.Arrival,
                                                    "12/26/2019",
                                                    numberOfAdult: Convert.ToInt32(searchFlightDetailsObject.TotalAdults),
                                                    numberOfChild: Convert.ToInt32(searchFlightDetailsObject.TotalChildren));

            //Search flights - Call Flight search endpoint
            var searchFlightsResult = RunAPI.PostSearchFlightAsync(RunAPI.SearchFlightEndPoint, searchFlightJsonObject).Result;

            //Deserialize flight results data
            dynamic deserializedSearchFlightList = JsonConvert.DeserializeObject(searchFlightsResult);
            var resultItineraries = deserializedSearchFlightList.body.data.itineraries;
            foreach (var itinerary in resultItineraries)
            {
                var originDestRefNumber = itinerary.origin_destinations[0].ref_number;
                var originDestDirectionId = itinerary.origin_destinations[0].direction_id;
                var originDestElapsedTime = itinerary.origin_destinations[0].elapsed_time;
                var originDestSegmentList = itinerary.origin_destinations[0].segments;
                var originDestOptionPricingInfo = itinerary.origin_destinations[0].option_pricing_info;

                var validatingAirlineCode = itinerary.validating_airline_code;
                var combinationId = itinerary.combination_id;
                var sequenceNumber = itinerary.sequence_number;
                var cabinInfo = itinerary.cabin;

                var pricingProvider = itinerary.pricing.provider;
                var pricingPortalFare = itinerary.pricing.portal_fare;
            }

            return Json(searchFlightsResult);
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
