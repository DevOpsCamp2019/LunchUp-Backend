using System.Threading.Tasks;
using LunchUp.Model.Models;

namespace LunchUp.Core.Integration
{
    public interface IIntegrationService
    {
        Task CreateOrUpdatePerson(PersonEntity person);
    }
}