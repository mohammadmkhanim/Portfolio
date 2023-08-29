using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data.Context;
using Portfolio.Data.Repository.IRepositories;
using Portfolio.Data.Repository.Repositories;

namespace Portfolio.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private PortfolioDBContext _context;

        public IRecentWorkRepository _recentWorkRepository;
        public IServiceRepository _serviceRepository;
        public ILanguageRepository _languageRepository;
        public IImageRepository _imageRepository;
        public ITechnicalSkillRepository _technicalSkillRepository;

        public UnitOfWork(PortfolioDBContext context)
        {
            _context = context;
        }

        public IRecentWorkRepository RecentWorkRepository
        {
            get
            {
                if (_recentWorkRepository == null)
                {
                    _recentWorkRepository = new RecentWorkRepository(_context);
                }
                return _recentWorkRepository;
            }
        }
        public IServiceRepository ServiceRepository
        {
            get
            {
                if (_serviceRepository == null)
                {
                    _serviceRepository = new ServiceRepository(_context);
                }
                return _serviceRepository;
            }
        }
        public ILanguageRepository LanguageRepository
        {
            get
            {
                if (_languageRepository == null)
                {
                    _languageRepository = new LanguageRepository(_context);
                }
                return _languageRepository;
            }
        }
        public IImageRepository ImageRepository
        {
            get
            {
                if (_imageRepository == null)
                {
                    _imageRepository = new ImageRepository(_context);
                }
                return _imageRepository;
            }
        }
        public ITechnicalSkillRepository TechnicalSkillRepository
        {
            get
            {
                if (_technicalSkillRepository == null)
                {
                    _technicalSkillRepository = new TechnicalSkillRepository(_context);
                }
                return _technicalSkillRepository;
            }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}