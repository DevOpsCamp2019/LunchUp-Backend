using System;
using LunchUp.Model.Models;

namespace LunchUp.Core.Integration
{
    public class IntegrationService : IIntegrationService
    {
        private static LunchUpContext _lunchUpContext;
        public IntegrationService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }

        public void CreateOrUpdatePerson(PersonEntity person)
        {
            throw new NotImplementedException();
        }
    }
}
