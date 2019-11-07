using System.Collections.Generic;
using System.Threading.Tasks;
using LunchUp.Model.Models;

namespace LunchUp.Core.Integration
{
    public interface IIntegrationService
    {
        Task CreateOrUpdatePersons(IEnumerable<PersonEntity> person);
    }
}