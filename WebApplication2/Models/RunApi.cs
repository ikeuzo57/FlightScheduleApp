using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2.Models
{
    public class RunAPI
    {

        public static string FlightCommonEndPoint = "http://www.ije-api.tcore.xyz";
        public static string CitiesTypeAheadEndPoint = FlightCommonEndPoint + "/v1/plugins/cities-type-ahead";
        public static string SearchFlightEndPoint = FlightCommonEndPoint + "/v1/flight/search-flight";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointUrl"></param>
        /// <returns></returns>
        public static async Task<string> GetFlightInfoAsync(string endpointUrl)
        {
            var httpClient = new HttpClient();
            var response =  httpClient.GetAsync(endpointUrl).Result;
            var result = await response.Content.ReadAsStringAsync();

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointUrl"></param>
        /// <param name="searchFlightCity"></param>
        /// <returns></returns>
        public static string GetFlightCityCode(string endpointUrl, string searchFlightCity)
        {
            //Check searchflightcity string
            var searchFlightArr = searchFlightCity.Split(',');

            var searchParam = searchFlightArr[0];
            try //Check if search param contains space
            {
                searchParam = searchFlightArr[0].Substring(0, searchFlightArr[0].IndexOf(' '));
            }
            catch { }

            var result = GetFlightInfoAsync(endpointUrl + "/" + searchParam).Result;

            dynamic deserializedCitiesResult = JsonConvert.DeserializeObject(result);

            foreach (var cityData in deserializedCitiesResult.data)
            {
                string cityName = (string)cityData.name;

                if (cityName.Trim().ToUpper() == searchFlightArr[0].Trim().ToUpper())
                {
                    return cityData.code;
                }
            }

            return string.Empty;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="departureCity"></param>
        /// <param name="destinationCity"></param>
        /// <param name="departureDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="numberOfAdult"></param>
        /// <param name="numberOfChild"></param>
        /// <param name="numberOfInfant"></param>
        /// <returns></returns>
        public static string FormatFlightSearchJsonObject(string departureCity, string destinationCity, string departureDate, string returnDate = "",
            int numberOfAdult = 1, int numberOfChild = 0, int numberOfInfant = 0)
        {
            dynamic jsonObject = new JObject();
            jsonObject.header = new JObject();
            jsonObject.header.cookie = "ayaeh33y1nw4yjtm3fdr0gzq";
            jsonObject.body = new JObject();
            jsonObject.body.origin_destinations = new JArray() as dynamic;
            dynamic destinationObj = new JObject();
            destinationObj.departure_city = departureCity.Trim();
            destinationObj.destination_city = destinationCity.Trim();
            destinationObj.departure_date = departureDate.Trim();
            destinationObj.return_date = returnDate.Trim();
            jsonObject.body.origin_destinations.Add(destinationObj);
            jsonObject.body.search_param = new JObject();
            jsonObject.body.search_param.no_of_adult = numberOfAdult;
            jsonObject.body.search_param.no_of_child = numberOfChild;
            jsonObject.body.search_param.no_of_infant = numberOfInfant;
            jsonObject.body.search_param.preferred_airline_code = "";
            jsonObject.body.search_param.calendar = false;
            jsonObject.body.search_param.cabin = "All";

            return jsonObject.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpointUrl"></param>
        /// <param name="searchJsonObject"></param>
        /// <returns></returns>
        public static async Task<string> PostSearchFlightAsync(string endpointUrl, string searchJsonObject)
        {
           
               
                using (var content = new StringContent(searchJsonObject, Encoding.UTF8, "application/json"))
                //HttpContent httpContent = CreateHttpContent(json);
                using (var client = new HttpClient())
                {
                    try
                    {
                    HttpResponseMessage response = await client.PostAsync(endpointUrl, content); 
                        

                        if (!response.IsSuccessStatusCode)
                        {
                            return string.Empty;
                        }
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return jsonResponse;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                                              
               //System.Diagnostics.Debug.WriteLine(jsonResponse);               
            
        }

    }
}