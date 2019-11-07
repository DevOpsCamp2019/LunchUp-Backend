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
            var currentPerson = _lunchUpContext.Person.FirstOrDefault(x => x.Id == person.Id);
            if (currentPerson != null)
            {
                currentPerson.Company = person.Company;
                currentPerson.Email = person.Email;
                currentPerson.Firstname = person.Firstname;
                currentPerson.Lastname = person.Lastname;
                currentPerson.Photo = person.Photo;
                _lunchUpContext.Update(currentPerson);
            }
            else
            {
                await _lunchUpContext.AddAsync(person);
            }

            await _lunchUpContext.SaveChangesAsync();
        }
    }
}