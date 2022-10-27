using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Model
{
    public class ActorModel
    {
        [Required]
        [MaxLength(45, ErrorMessage ="Độ dài tối đa là 45 ký tự")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(45, ErrorMessage = "Độ dài tối đa là 45 ký tự")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage ="Vui lòng nhập đúng định dạng email!")]
        public string Email { get; set; }
        public DateTime Dob { get; set; }
    }
}
