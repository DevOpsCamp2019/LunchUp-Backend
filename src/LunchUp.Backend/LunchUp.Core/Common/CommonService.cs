using System.Linq;
using LunchUp.Model;

namespace LunchUp.Core.Common
{
    public class CommonService : ICommonService
    {
        private static LunchUpContext _lunchUpContext;

        public CommonService(LunchUpContext lunchUpContext)
        {
            _lunchUpContext = lunchUpContext;
        }

        public bool GetPersonExistStatus(string currentUserMail)
        {
            var userExists = _lunchUpContext.Person.Any(x => x.Email == currentUserMail);
            return userExists;
        }
    }
}