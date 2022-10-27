using System.Collections.Generic;
using WebApplication3.DBContext;
using WebApplication3.Model;
using WebApplication3.Repository;

namespace WebApplication3.Services
{
    public class CustomerServices
    {
        private CustomerRepository customerRepository;
        public CustomerServices()
        {
            customerRepository = new CustomerRepository();
        }

        public Customer Create(CustomerModel cus)
        {
            return customerRepository.Create(cus);
        }
    }
}
