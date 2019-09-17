using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class FlightResult
    {
        private List<FlightSegment> _flightSegmentList;
        private List<FlightPrice> _flightPriceList;

        public string ElapsedTime { get; set; }
        public List<FlightSegment> FlightSegmentList
        {
            get
        {
            if(_flightSegmentList == null)
            {
                _flightSegmentList = new List<FlightSegment>();
            }

            return _flightSegmentList;
        }

            set
            {
                _flightSegmentList = value;
            }
        }
        public string Cabin { get; set; }
        public string PriceCurrency { get; set; }
        public string BaseFarePrice { get; set; }
        public string TotalFarePrice { get; set; }
        public List<FlightPrice> FlightPriceList
        {
            get
            {
                if (_flightPriceList == null)
                {
                    _flightPriceList = new List<FlightPrice>();
                }

                return _flightPriceList;
            }

            set
            {
                _flightPriceList = value;
            }
        }
    }


    public class FlightSegment
    {
        public string DepartureDate { get; set; }
        public string DepartureTime { get; set; }
        public string DepartureAirport { get; set; }
        public string DepartureTerminal { get; set; }
        public string ArrivalDate { get; set; }
        public string ArrivalTime { get; set; }
        public string ArrivalAirport { get; set; }
        public string ArrivalTerminal { get; set; }
        public string FlightNumber { get; set; }
        public string FlightDuration { get; set; }
        public string OperatingAirline { get; set; }
        public string MarketingAirline { get; set; }
        public string BaggageAllowance { get; set; }
    }


    public class FlightPrice
    {
        public string PassengerCode { get; set; }
        public string PassengerQty { get; set; }
        public string PassengerBaseFarePrice { get; set; }
        public string PassengerFareTax { get; set; }
        public string TotalPassengerFarePrice { get; set; }
    }
}