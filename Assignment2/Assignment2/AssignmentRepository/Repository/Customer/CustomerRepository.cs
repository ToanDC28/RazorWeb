using Assignment2Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentRepository.Repository.Customer
{
    public class CustomerRepository : ICustomerRepo
    {
        private PRN_Assignment2Context _context;
        public CustomerRepository(PRN_Assignment2Context context)
        {
            _context = context;
        }

        public void Add(TlCustomer entity)
        {
            _context.Add(entity);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TlCustomer> GetAll()
        {
            throw new NotImplementedException();
        }

        public TlCustomer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TlCustomer Login(string phone, string password)
        {
            throw new NotImplementedException();
        }

        public void Update(TlCustomer entity)
        {
            throw new NotImplementedException();
        }
        //implements ICustomerRepo
    }
}
