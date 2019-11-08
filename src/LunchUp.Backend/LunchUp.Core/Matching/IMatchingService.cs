using System;
using System.Collections.Generic;
using LunchUp.Model.Models;

namespace LunchUp.Core.Matching
{
    public interface IMatchingService
    {
        List<PersonEntity> GetSuggestions(PersonEntity user, int count = 10);
        List<PersonEntity> GetMatches(PersonEntity user);
        void AddMatch(PersonEntity user, Guid personId, bool accepted);
    }
}