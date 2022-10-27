using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication3.Model
{
    public class CustomerModel
    {
        public byte StoreId { get; set; }
        public string FirstName { get; set; }
        [MinLength(1), MaxLength(45), NotNull]
        public string LastName { get; set; }
        public string Email { get; set; }
        public short AddressId { get; set; }
        public bool? Active { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
    }
}
