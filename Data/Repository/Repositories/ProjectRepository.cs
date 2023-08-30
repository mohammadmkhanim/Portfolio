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
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private DbSet<Project> _recentWorks;
        public ProjectRepository(PortfolioDBContext context) : base(context)
        {
            _recentWorks = context.Projects;
        }
    }
}