using System.Collections.Generic;
using System.Linq;

namespace FoodApiSearchAW
{
    public class DataManager
    {
        // Sort search result with regards to Calories, Fat & Protein in descending order
        public static List<FoodItem> SortSearchResult(List<FoodItem> searchResultList)
        {
            return searchResultList.OrderByDescending(x => x.Calories)
              .ThenByDescending(x => x.Fat)
              .ThenByDescending(x => x.Protein)
              .ToList();
        }
    }
}
