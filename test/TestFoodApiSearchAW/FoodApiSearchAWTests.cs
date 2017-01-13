using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodApiSearchAW;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFoodApiSearchAW
{
    [TestClass]
    public class FoodApiSearchAWTests
    {

        [TestMethod]
        public async Task FoodApiSearch_ShouldReturnCorrectList()
        {
            List<FoodItem> testSearch = await FoodApi.Search("apa");
            Assert.IsNotNull(testSearch);
            Assert.AreEqual(5, testSearch.Count());
        }

        [TestMethod]
        public async Task FoodApiSearch_ShouldReturnCorrectNutrients()
        {
            var expected = new FoodItem()
            {
                Id = 613,
                Name = "Papaya torkad",
                Calories = 263,
                Fat = 1,
                Protein = 3
            };
            string input = "papaya torkad";
            List<FoodItem> actual = await FoodApi.Search(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Calories, actual[0].Calories);
            Assert.AreEqual(expected.Fat, actual[0].Fat);
            Assert.AreEqual(expected.Protein, actual[0].Protein);
        }


        [TestMethod]
        public void SortSearchResult_ShouldReturnListSortedDescending()
        {
            var expected = new string[] { "B", "A", "C" };

            // The following is the test data for the test method
            var input = new List<FoodItem>()
            {
                new FoodItem() { Name = "A",  Calories = 263, Fat = 1, Protein = 3 },
                new FoodItem(){ Name = "B", Calories = 403, Fat = 6, Protein = 9  },
                new FoodItem() { Name = "C", Calories = 228, Fat = 1, Protein = 7 }
            };

            IEnumerable<string> actual = (DataManager.SortSearchResult(input)).Select(m => m.Name);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count());
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}
