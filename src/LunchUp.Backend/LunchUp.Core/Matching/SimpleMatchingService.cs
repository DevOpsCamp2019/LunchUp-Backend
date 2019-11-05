using System;
using System.Collections.Generic;
using System.Linq;
using LunchUp.Model.Models;

namespace LunchUp.Core.Matching
{
    public class SimpleMatchingService : IMatchingService
    {
        private static LunchUpContext _lunchUpContext;
        public SimpleMatchingService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }
        
        public List<PersonEntity> GetSuggestions(int count = 10)
        {
            var person = _lunchUpContext.Person.OrderBy(r => Guid.NewGuid()).Take(count).ToList();
            return person;
        }

        public List<PersonEntity> GetMatches()
        {
            var matches = SampleData.Suggestions().Take(2).ToList();
            return matches;
        }
    }
}