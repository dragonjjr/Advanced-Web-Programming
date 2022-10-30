using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Common
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập UserName")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ResponMessages
    {
        public int Status { get; set; }
        public string messages { get; set; }
        public object Data { get; set; }
        public string accesstoken { get; set; }
        public string refreshtoken { get; set; }
    }

    public class Actor
    {
        public Actor()
        {
            FilmActors = new HashSet<FilmActor>();
        }

        public short ActorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }

        public virtual ICollection<FilmActor> FilmActors { get; set; }
    }

    public partial class FilmActor
    {
        public short ActorId { get; set; }
        public short FilmId { get; set; }

        public virtual Actor Actor { get; set; }
    }

    public class UserRefreshToken
    {
        public string User { get; set; }
        public string token { get; set; }
    }

    public class Market
    {
        public string? CompanyName { get; set; }
        public int Volume { get; set; }
    }
}
