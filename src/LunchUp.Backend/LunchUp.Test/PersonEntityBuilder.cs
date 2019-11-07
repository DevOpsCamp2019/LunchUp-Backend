using LunchUp.Model;
using LunchUp.Model.Models;
using System;
using System.Collections.Generic;

namespace LunchUp.Test
{
    public class PersonEntityBuilder
    {
        private readonly LunchUpContext LunchUpContext;
        private Guid Id;
        private string Lastname;
        private string Firstname;
        private string Email;
        private string Photo;
        private string Company;
        private DateTime? OptIn;
        private ICollection<ResponseEntity> Responses;

        public PersonEntityBuilder(LunchUpContext lunchUpContext, string email)
        {
            LunchUpContext = lunchUpContext;
            Id = Guid.NewGuid();
            Email = email;
        }

        public PersonEntityBuilder WithId(Guid id)
        {
            Id = id;
            return this;
        }

        public PersonEntityBuilder WithLastname(string firstName)
        {
            Firstname = firstName;
            return this;
        }

        public PersonEntityBuilder WithFirstname(string lastname)
        {
            Lastname = lastname;
            return this;
        }

        public PersonEntityBuilder WithEmail(string email)
        {
            Email = email;
            return this;
        }

        public PersonEntityBuilder WithPhoto(string photo)
        {
            Photo = photo;
            return this;
        }

        public PersonEntityBuilder WithCompany(string company)
        {
            Company = company;
            return this;
        }

        public PersonEntityBuilder WithOptIn(DateTime? optIn)
        {
            OptIn = optIn;
            return this;
        }

        public PersonEntityBuilder WithResponses(ICollection<ResponseEntity> responses)
        {
            Responses = responses;
            return this;
        }

        public PersonEntity BuildSaved()
        {
            var entity = new PersonEntity
            {
                Id = this.Id,
                Lastname = this.Lastname,
                Firstname = this.Firstname,
                Email = this.Email,
                Photo = this.Photo,
                Company = this.Company,
                OptIn = this.OptIn,
                Responses = this.Responses
            };

            LunchUpContext.Person.Add(entity);
            LunchUpContext.SaveChanges();
            return entity;
        }

        public PersonEntity BuildUnsaved()
        {
            var entity = new PersonEntity
            {
                Id = this.Id,
                Lastname = this.Lastname,
                Firstname = this.Firstname,
                Email = this.Email,
                Photo = this.Photo,
                Company = this.Company,
                OptIn = this.OptIn,
                Responses = this.Responses
            };
            return entity;
        }
    }
}
