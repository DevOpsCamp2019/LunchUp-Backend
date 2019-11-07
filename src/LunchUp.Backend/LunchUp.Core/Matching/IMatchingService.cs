using System;
using System.Collections.Generic;
using LunchUp.Model.Models;

namespace LunchUp.Core.Matching
{
    public interface IMatchingService
    {
        List<PersonEntity> GetSuggestions(string currentUserMail, int count = 10);
        List<PersonEntity> GetMatches(PersonEntity currentUser);
        void AddMatch(string currentUserUpn, Guid personId, bool accepted);
    }
}