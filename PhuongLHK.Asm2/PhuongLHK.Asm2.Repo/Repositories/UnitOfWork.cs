using PhuongLHK.Asm2.Repo.Models;
using System;
using System.Threading.Tasks;

namespace PhuongLHK.Asm2.Repo.Repositories
{
    public class UnitOfWork : IDisposable
    {
        public PRN_Assignment2Context _context { get; }

        private IRepository<TlAccount> accRepository;
        public IRepository<TlAccount> accountRepository
        {
            get
            {
                if (accRepository == null)
                {
                    accRepository = new Repository<TlAccount>();
                }
                return accRepository;
            }
        }

        private IRepository<Category> cateRepository;
        public IRepository<Category> categoryRepository { 
            get 
            {
                if (cateRepository == null)
                {
                    cateRepository = new Repository<Category>();
                }
                return cateRepository;
            }
        }

        private IRepository<Customer> cusRepository;
        public IRepository<Customer> customerRepository {
            get 
            {
                if (cusRepository == null)
                {
                    cusRepository = new Repository<Customer>();
                }
                return cusRepository;
            }
        }
        private IRepository<Supplier> supRepository;
        public IRepository<Supplier> supplierRepository {
            get 
            {
                if (supRepository == null)
                {
                    supRepository = new Repository<Supplier>();
                }
                return supRepository;
            }
        }

        private IRepository<Product> proRepository;
        public IRepository<Product> productRepository {
            get
            {
                if (proRepository == null)
                {
                    proRepository = new Repository<Product>();
                }
                return proRepository;
            }
        }
        private IRepository<Order> oRepository;
        public IRepository<Order> orderRepository {
            get
            {
                if (oRepository == null)
                {
                    oRepository = new Repository<Order>();
                }
                return oRepository;
            }
        }

        private IRepository<OrderDetail> odRepository;
        public IRepository<OrderDetail> orderDetailRepository {
            get
            {
                if (odRepository == null)
                {
                    odRepository = new Repository<OrderDetail>();
                }
                return odRepository;
            }
        }
        public UnitOfWork(PRN_Assignment2Context context
            )
        {
            _context = context;

            accountRepository.DbContext = _context;

            categoryRepository.DbContext = _context;

            customerRepository.DbContext = _context;

            supplierRepository.DbContext = _context;

            productRepository.DbContext = _context;

            orderRepository.DbContext = _context;

            orderDetailRepository.DbContext = _context;

        }

        public int SaveChanges()
        {
            var iResult = _context.SaveChanges();
            return iResult;
        }

        public async Task<int> SaveChangesAsync()
        {
            var iResult = await _context.SaveChangesAsync();
            return iResult;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
