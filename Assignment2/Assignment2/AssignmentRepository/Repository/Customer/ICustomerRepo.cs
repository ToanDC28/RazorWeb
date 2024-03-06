using Assignment2Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment2Entity.Models;

namespace AssignmentRepository.Repository.Customer
{
    public interface ICustomerRepo
    {
        IEnumerable<TlCustomer> GetAll();
        TlCustomer GetById(int id);
        TlCustomer Login(string phone, string password);
        void Add(TlCustomer entity);
        void Update(TlCustomer entity);
        void Delete(int id);
    }
}
