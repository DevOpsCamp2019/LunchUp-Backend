using System;
using System.Collections.Generic;
using System.Text;
using LunchUp.Model;

namespace LunchUp.Core
{
    public interface IMatchingService
    {
        List<PersonEntity> GetSuggestions(int count = 10);
        List<PersonEntity> GetMatches();
    }
}
