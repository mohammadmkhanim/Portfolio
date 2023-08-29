using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Context;
using Portfolio.Data.Entities;
using Portfolio.Data.Repository.GenericRepository;
using Portfolio.Data.Repository.IRepositories;

namespace Portfolio.Data.Repository.Repositories
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        private DbSet<Language> _languages;
        public LanguageRepository(PortfolioDBContext context) : base(context)
        {
            _languages = context.Languages;
        }

    }
}