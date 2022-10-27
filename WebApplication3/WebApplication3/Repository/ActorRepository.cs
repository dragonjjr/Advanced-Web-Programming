using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication3.DBContext;
using WebApplication3.Model;

namespace WebApplication3.Repository
{
    public class ActorRepository
    {
        private testapiContext testapiContext;
        public ActorRepository()
        {
            testapiContext = new testapiContext();
        }

        public List<Actor> GetAll()
        {
            return testapiContext.Actors.ToList();
        }

        public Actor Create(ActorModel actor)
        {
            Actor actor2 = new Actor
            {
                FirstName = actor.FirstName,
                LastName = actor.LastName,
                Email = actor.Email,
                Dob = actor.Dob,
            };
            testapiContext.Actors.Add(actor2);
            var rs = testapiContext.SaveChanges();
            return actor2;

        }
    }
}
