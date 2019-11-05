using LunchUp.Model.Models;

namespace LunchUp.Core.Integration
{
    public interface IIntegrationService
    {
        void CreateOrUpdatePerson(PersonEntity person);
    }
}
