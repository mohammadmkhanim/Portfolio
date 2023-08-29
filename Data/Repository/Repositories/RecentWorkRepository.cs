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
    public class RecentWorkRepository : GenericRepository<RecentWork>, IRecentWorkRepository
    {
        private DbSet<RecentWork> _recentWorks;
        public RecentWorkRepository(PortfolioDBContext context) : base(context)
        {
            _recentWorks = context.RecentWorks;
        }
    }
}