using Assignment2Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentRepository.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        private PRN_Assignment2Context _context;

        public AccountRepository()
        {
        }

        public AccountRepository(PRN_Assignment2Context context)
        {
            _context = context;
        }

        public TlAccount login(string username, string password)
        {
            return _context.Accounts.Where(a => a.UserName == username && a.Password == password).FirstOrDefault();
        }

        public void CreateUserAsync(TlAccount account)
        {
            //check if username is already taken
            if (_context.Accounts.Any(a => a.UserName == account.UserName))
            {
                throw new Exception("Username is already taken");
            }
            _context.Accounts.Add(account);
        }
    }
}
