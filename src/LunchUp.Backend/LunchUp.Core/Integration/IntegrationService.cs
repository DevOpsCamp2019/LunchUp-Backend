using System;
using System.Linq;
using System.Threading.Tasks;
using LunchUp.Model;
using LunchUp.Model.Models;

namespace LunchUp.Core.Integration
{
    public class IntegrationService : IIntegrationService
    {
        private readonly LunchUpContext _lunchUpContext;

        public IntegrationService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }

        public async Task CreateOrUpdatePerson(PersonEntity person)
        {
            var currentPerson = _lunchUpContext.Person.FirstOrDefault(x => x.Email == person.Email);
            if (currentPerson != null)
            {
                currentPerson.Company = person.Company;
                currentPerson.Firstname = person.Firstname;
                currentPerson.Lastname = person.Lastname;
                currentPerson.Photo = person.Photo;
                
                // TODO: Remove in production
                if(currentPerson.OptIn == null) person.OptIn = DateTime.UtcNow;
                
                _lunchUpContext.Update(currentPerson);
            }
            else
            {
                person.Id = Guid.NewGuid();
                // TODO: Remove in production
                person.OptIn = DateTime.UtcNow;
                await _lunchUpContext.AddAsync(person);
            }

            await _lunchUpContext.SaveChangesAsync();
        }
    }
}