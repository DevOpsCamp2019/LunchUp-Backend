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
            // ReSharper disable once SpecifyStringComparison (not compatible with linq to sql)
            var user = _lunchUpContext.Person.FirstOrDefault(x => x.Email.ToLower() == currentUserMail.ToLower());
            if (user != null && user.OptIn == null)
            {
                user.OptIn = DateTime.UtcNow;
                _lunchUpContext.Update(user);
                _lunchUpContext.SaveChanges();
            }
            
            return user;
        }
    }
}