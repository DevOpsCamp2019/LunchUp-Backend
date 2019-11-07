using System;
using System.Linq;
using LunchUp.Model;
using LunchUp.Model.Models;

namespace LunchUp.Core.Common
{
    public class CommonService : ICommonService
    {
        private readonly LunchUpContext _lunchUpContext;

        public CommonService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }

        public PersonEntity GetPersonExistStatus(string currentUserMail)
        {
            var user = _lunchUpContext.Person.FirstOrDefault(x => x.Email == currentUserMail);
            if (user != null)
            {
                user.OptIn = DateTime.UtcNow;
                _lunchUpContext.Update(user);
                _lunchUpContext.SaveChanges();
            }
            
            return user;
        }
    }
}