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
            var currentResponses = _lunchUpContext.Person
                .Include(x => x.Responses).ThenInclude(x => x.Target)
                .FirstOrDefault(x => x.Email == currentUserMail)?.Responses
                .Select(x => x.Target.Id).AsEnumerable();
            
            var persons = _lunchUpContext.Person
                .Where(entity => entity.Email != currentUserMail && entity.OptIn != null && !currentResponses.Contains(entity.Id))
                .AsEnumerable()
                .OrderBy(x => Guid.NewGuid())
                .Take(count).ToList();
            
            return persons;
        }
        
        public List<PersonEntity> GetMatches(string currentUserUpn)
        {
            var matches = _lunchUpContext.Response.Include(x => x.Origin)
                .Where(entity => entity.Origin.Email == currentUserUpn && entity.Like)
                .Select(x => x.Target).ToList();
            return matches;
        }
        
        public void AddMatch(string currentUserUpn, Guid personId, bool accepted)
        {
            var existingResponse =
                _lunchUpContext.Response.FirstOrDefault(
                    x => x.Origin.Email == currentUserUpn && x.Target.Id == personId);
            if (existingResponse != null)
            {
                existingResponse.Like = accepted;
                existingResponse.ResponseDate = DateTime.UtcNow;
                _lunchUpContext.Update(existingResponse);
            }
            else
            {
                var response = new ResponseEntity();
                response.Id = Guid.NewGuid();
                response.Like = accepted;
                response.Origin = _lunchUpContext.Person.First(x => x.Email == currentUserUpn);
                response.Target = _lunchUpContext.Person.First(x => x.Id == personId);
                response.ResponseDate = DateTime.UtcNow;
                _lunchUpContext.Add(response);
            }
            
            _lunchUpContext.SaveChanges();
        }
    }
}