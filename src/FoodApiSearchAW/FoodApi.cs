using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FoodApiSearchAW
{
    public class FoodApi
    {
        static string webApiUrl = "http://www.matapi.se/foodstuff";
        static List<FoodItem> searchResultList;

        public static async Task<List<FoodItem>> Search(string searchTerm)
        {
            searchResultList = await GetFullSearchResult(searchTerm);
            return await GetDetailedSearchResult(searchResultList);
        }

        private static async Task<List<FoodItem>> GetFullSearchResult(string searchTerm)
        {
            // Execute call to Api 
            var webAPiUrlFull = $"{webApiUrl}?query={searchTerm}";
            var json = await GetHttpAsync(webAPiUrlFull);
            JArray o = JArray.Parse(json);

            searchResultList = new List<FoodItem>();

            // Save ID and Name for all search hits
            foreach (var ob in o)
            {
                var foodItem = new FoodItem();

                // Break out Name and Number for each item in searchlist and add to list
                foodItem.Name = (string)ob["name"];
                foodItem.Id = (int)ob["number"];
                searchResultList.Add(foodItem);
            }

            return searchResultList;
        }

        private static async Task<string> GetHttpAsync(string url)
        {
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return json;
        }

        private static async Task<List<FoodItem>> GetDetailedSearchResult(List<FoodItem> searchResultList)
        {
            // Loop through list of search result and get calories, protein and fat for each 
            foreach (var foodItem in searchResultList)
            {
                var webApiUrlDetailed = $"{webApiUrl}/{foodItem.Id}?nutrient";
                var json = await GetHttpAsync(webApiUrlDetailed);
                JObject o = JObject.Parse(json);

                // Add nutrients to the object
                foodItem.Protein = (int)o["nutrientValues"]["protein"];
                foodItem.Fat = (int)o["nutrientValues"]["fat"];
                foodItem.Calories = (int)o["nutrientValues"]["energyKcal"];
            }

            return searchResultList;
        }
    }
}
