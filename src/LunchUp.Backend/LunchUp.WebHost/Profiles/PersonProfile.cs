using AutoMapper;
using LunchUp.Model;
using LunchUp.WebHost.Dto;

namespace LunchUp.WebHost.Profiles
{
    /// <inheritdoc />
    public class PersonProfile : Profile
    {
        /// <inheritdoc />
        public PersonProfile()
        {
            CreateMap<PersonEntity, Person>();
        }
    }
}
