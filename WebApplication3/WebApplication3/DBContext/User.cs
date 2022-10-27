using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication3.DBContext
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Refreshtoken { get; set; }
        public DateTime? Refreshtokenexpirytime { get; set; }
    }
}
