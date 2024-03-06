using Assignment2Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentRepository.Repository.Account
{
    public interface IAccountRepository
    {
        TlAccount login(string username, string password);
    }
}
