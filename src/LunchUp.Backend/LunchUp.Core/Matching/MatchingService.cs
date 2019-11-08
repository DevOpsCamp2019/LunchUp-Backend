using System;
using System.Collections.Generic;
using System.Linq;
using LunchUp.Model;
using LunchUp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace LunchUp.Core.Matching
{
    public class MatchingService : IMatchingService
    {
        private readonly LunchUpContext _lunchUpContext;

        public MatchingService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }

        public List<PersonEntity> GetSuggestions(PersonEntity user, int count = 10)
        {
            var currentResponses = _lunchUpContext.Person
                .Include(x => x.Responses).ThenInclude(x => x.Target)
                .FirstOrDefault(x => x.Id == user.Id)?.Responses
                .Select(x => x.Target.Id).AsEnumerable();

            var persons = _lunchUpContext.Person
                .Where(entity => entity.Id != user.Id && entity.OptIn != null &&
                                 (currentResponses == null || !currentResponses.Contains(entity.Id)))
                .AsEnumerable()
                .OrderBy(x => Guid.NewGuid())
                .Take(count).ToList();

            return persons;
        }

        public List<PersonEntity> GetMatches(PersonEntity user)
        {
            var matches = from response1 in _lunchUpContext.Response
                join response2 in _lunchUpContext.Response on response1.Origin equals response2.Target
                where response1.Like && response2.Like && response2.Origin == response1.Target && response1.Target != user
                select response1.Target;
            
            return matches.ToList();
        }

        public void AddMatch(PersonEntity user, Guid personId, bool accepted)
        {
            var existingResponse =
                _lunchUpContext.Response.FirstOrDefault(
                    x => x.Origin.Id == user.Id && x.Target.Id == personId);
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
                response.Origin = _lunchUpContext.Person.First(x => x.Id == user.Id);
                response.Target = _lunchUpContext.Person.First(x => x.Id == personId);
                response.ResponseDate = DateTime.UtcNow;
                _lunchUpContext.Add(response);
            }

            _lunchUpContext.SaveChanges();
        }
    }
}