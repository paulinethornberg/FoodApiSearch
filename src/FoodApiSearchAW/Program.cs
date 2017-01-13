using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodApiSearchAW
{
    public class Program
    {
        static string searchTerm;

        public static void Main(string[] args)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            ConsoleHandler.PrintIntroText(); 

            Task.Run(async () =>
            {
                await RunProgram();
            }).Wait();
        }

        private static async Task RunProgram()
        {
            var searchResultList = new List<FoodItem>();

            searchTerm = ConsoleHandler.GetUserSearchInput();

            searchResultList = await FoodApi.Search(searchTerm);
            searchResultList = DataManager.SortSearchResult(searchResultList);
            ConsoleHandler.PrintSearchResult(searchResultList);

            // Check if user wants to make a new search, else quit program
            while (true)
            {
                string answer = ConsoleHandler.AskIfNewSearch();
                if (answer == "Y")
                {
                    await RunProgram();
                }
                else if (answer == "N")
                {
                    break;
                }
            }
        }
    }
}
