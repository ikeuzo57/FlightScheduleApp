using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SearchFlightDetails
    {
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string DepartureDate { get; set; }
        public string ArrivalDate { get; set; }
        public string RoundTrip  { get; set; }
        public string OneWay { get; set; }
        public string TotalAdults { get; set; }
        public string TotalChildren { get; set; }
        public int TotalInfants { get; set; }
    }
}