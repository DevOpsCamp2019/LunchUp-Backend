using LunchUp.Model;
using LunchUp.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LunchUp.Test
{
    public class ResponseBuilder
    {
        private readonly LunchUpContext LunchUpContext;
        private Guid Id;
        private PersonEntity Origin;
        private PersonEntity Target;
        private bool Like;
        private DateTime ResponseDate;

        public ResponseBuilder(LunchUpContext lunchUpContext, PersonEntity origin, PersonEntity target, bool like, DateTime responseDate)
        {
            LunchUpContext = lunchUpContext;
            Id = Guid.NewGuid();
            Origin = origin;
            Target = target;
            Like = like;
            ResponseDate = responseDate;
        }

        public ResponseEntity BuildSaved()
        {
            var entity = new ResponseEntity
            {
                Id = this.Id,
                Origin = this.Origin,
                Target = this.Target,
                Like = this.Like,
                ResponseDate = this.ResponseDate
            };

            LunchUpContext.Response.Add(entity);
            LunchUpContext.SaveChanges();
            return entity;
        }
    }
}
