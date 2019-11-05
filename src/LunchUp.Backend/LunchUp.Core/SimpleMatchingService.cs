using System.Collections.Generic;
using System.Linq;
using LunchUp.Model;
using LunchUp.Model.Models;

namespace LunchUp.Core
{
    public class SimpleMatchingService : IMatchingService
    {
        public List<PersonEntity> GetSuggestions(int count = 10)
        {
            var suggestions = SampleData.Suggestions();
            return suggestions;
        }

        public List<PersonEntity> GetMatches()
        {
            var matches = SampleData.Suggestions().Take(2).ToList();
            return matches;
        }
    }
}
