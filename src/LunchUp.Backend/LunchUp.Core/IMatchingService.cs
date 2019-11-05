using System;
using System.Collections.Generic;
using System.Text;
using LunchUp.Model;
using LunchUp.Model.Models;

namespace LunchUp.Core
{
    public interface IMatchingService
    {
        List<PersonEntity> GetSuggestions(int count = 10);
        List<PersonEntity> GetMatches();
    }
}
