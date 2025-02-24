﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication3.DBContext
{
    public partial class Customer
    {
        public Customer()
        {
            Payments = new HashSet<Payment>();
            Rentals = new HashSet<Rental>();
        }

        public short CustomerId { get; set; }
        public byte StoreId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public short AddressId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
