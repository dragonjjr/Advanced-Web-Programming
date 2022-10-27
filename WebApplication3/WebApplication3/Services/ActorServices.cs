using System.Collections.Generic;
using WebApplication3.DBContext;
using WebApplication3.Model;
using WebApplication3.Repository;

namespace WebApplication3.Services
{
    public class ActorServices
    {
        private ActorRepository actorRepository;
        public ActorServices()
        {
            actorRepository = new ActorRepository();
        }
        public List<Actor> GetAll()
        {
            return actorRepository.GetAll();
        }

        public Actor Create(ActorModel actor)
        {
            return actorRepository.Create(actor);
        }
    }
}
