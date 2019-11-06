using System;
using System.Collections.Generic;
using System.Linq;
using LunchUp.Model;
using LunchUp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace LunchUp.Core.Matching
{
    public class SimpleMatchingService : IMatchingService
    {
        private static LunchUpContext _lunchUpContext;
        public SimpleMatchingService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }
        
        public List<PersonEntity> GetSuggestions(string currentUserMail, int count)
        {
            var person = _lunchUpContext.Person.Where(entity => entity.Email != currentUserMail && entity.OptIn != null).OrderBy(r => Guid.NewGuid()).Take(count).ToList();
            return person;
        }

        public List<PersonEntity> GetMatches(string currentUserUpn)
        {
            var matches = _lunchUpContext.Response.Include(x => x.Origin)
                .Where(entity => entity.Origin.Email == currentUserUpn && entity.Like == true).Select(x => x.Target).ToList();
            return matches;
        }
        
        public void AddMatch(string currentUserUpn, Guid personId, bool accepted)
        {
            var response = new ResponseEntity();
            response.Id = Guid.NewGuid();
            response.Like = accepted;
            response.Origin = _lunchUpContext.Person.First(x => x.Email == currentUserUpn);
            response.Target = _lunchUpContext.Person.First(x => x.Id == personId);
            response.ResponseDate = DateTime.UtcNow;
            _lunchUpContext.Add(response);
            _lunchUpContext.SaveChanges();
        }
    }
}