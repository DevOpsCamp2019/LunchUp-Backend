using AutoMapper;
using LunchUp.Model.Models;
using LunchUp.WebHost.Dto;

namespace LunchUp.WebHost.Profiles
{
    /// <inheritdoc />
    // ReSharper disable once UnusedMember.Global
    public class PersonProfile : Profile
    {
        /// <inheritdoc />
        public PersonProfile()
        {
            CreateMap<PersonEntity, Person>();
            CreateMap<Person, PersonEntity> (); 
        }
    }
}