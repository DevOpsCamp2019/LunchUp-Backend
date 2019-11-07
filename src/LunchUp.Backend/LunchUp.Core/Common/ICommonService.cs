using LunchUp.Model.Models;

namespace LunchUp.Core.Common
{
    public interface ICommonService
    {
        PersonEntity GetPersonExistStatus(string currentUserUpn);
    }
}