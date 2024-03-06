﻿using Eyeglasses_DoanCongToan.Repo.Models;
using System;
using System.Threading.Tasks;

namespace Eyeglasses_DoanCongToan.Repo.Repositories
{
    public class UnitOfWork : IDisposable
    {
        public Eyeglasses2024DBContext _context { get; }

        private IRepository<Eyeglass> eGRepository;
        public IRepository<Eyeglass> eyeGlassRepository
        {
            get
            {
                if (eGRepository == null)
                {
                    eGRepository = new Repository<Eyeglass>();
                }
                return eGRepository;
            }
        }

        private IRepository<LensType> lTRepository;
        private IRepository<LensType> lenTypeRepository { 
            get 
            {
                if (lTRepository == null)
                {
                    lTRepository = new Repository<LensType>();
                }
                return lTRepository;
            }
        }

        private IRepository<StoreAccount> accRepository;
        private IRepository<StoreAccount> StoreAccRepository {
            get 
            {
                if (accRepository == null)
                {
                    accRepository = new Repository<StoreAccount>();
                }
                return accRepository;
            }
        }
        public UnitOfWork(Eyeglasses2024DBContext context
            )
        {
            _context = context;

            eyeGlassRepository.DbContext = _context;

            lenTypeRepository.DbContext = _context;

            StoreAccRepository.DbContext = _context;

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
