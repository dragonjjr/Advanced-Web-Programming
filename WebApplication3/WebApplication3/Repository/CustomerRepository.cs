using System.Collections.Generic;
using WebApplication3.DBContext;
using WebApplication3.Model;

namespace WebApplication3.Repository
{
    public class CustomerRepository
    {
        private testapiContext testapiContext;
        public CustomerRepository()
        {
            testapiContext = new testapiContext();
        }

        public Customer Create(CustomerModel cus)
        {
            Customer new_cus = new Customer { 
                StoreId = cus.StoreId,
                FirstName = cus.FirstName,
                LastName = cus.LastName,
                Email = cus.Email,
                AddressId = cus.AddressId,
                Active = cus.Active,
                CreateDate = cus.CreateDate
            };
            testapiContext.Customers.Add(new_cus);
            testapiContext.SaveChanges();
            return new_cus;
        }
    }
}
