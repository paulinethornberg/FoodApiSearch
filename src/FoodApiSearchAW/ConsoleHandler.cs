using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace FoodApiSearchAW
{
    public class ConsoleHandler
    {
        public static string GetUserSearchInput()
        {
            while (true)
            {
                string searchTerm;
                Console.WriteLine("Input searchword: ");
                searchTerm = Console.ReadLine();

                // Validate that searchword is a string 
                if (searchTerm.All(char.IsLetterOrDigit) || searchTerm.Contains(" "))
                {
                    return searchTerm;
                }
                else
                {
                    Console.WriteLine("The searchword may only contain alphabetical characters");
                }
            }
        }

        internal static void PrintIntroText()
        {
           Console.WriteLine("-----------------------------------------FOOD SEARCH API-----------------------------------------");
        }

        public static void PrintSearchResult(List<FoodItem> searchResultList)
        {
            // If no results on search, let the user know and print empty  table
            // then check if too many results, if larger than 20, show only first 20 hits
            // if user wants to see more, they can press enter to see all results
            if (searchResultList.Count() <= 0)
            {
                ConsoleTable.From<FoodItem>(searchResultList).Write();
                Console.WriteLine("------ No results found -------");
            }
            else if (searchResultList.Count() > 20)
            {
                List<FoodItem> searchResultListTopResults = searchResultList.GetRange(0, 20);

                //Package for creating nice tables in the console
                ConsoleTable.From<FoodItem>(searchResultListTopResults).Write();
                Console.WriteLine($"There are {searchResultList.Count()} results in total, press enter to see all or any other key to continue");

                ConsoleKeyInfo keyInfo = System.Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    ConsoleTable.From<FoodItem>(searchResultList).Write();
                }
            }
            else
            {
                ConsoleTable.From<FoodItem>(searchResultList).Write();
            }
        }

        internal static string AskIfNewSearch()
        {
            Console.Write("Do you want to make a new search [Y/N]?");
            return Console.ReadLine().ToUpper();
        }
    }
}
