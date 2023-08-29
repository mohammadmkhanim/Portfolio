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
    public class TechnicalSkillRepository : GenericRepository<TechnicalSkill>, ITechnicalSkillRepository
    {
        private DbSet<TechnicalSkill> _technicalSkills;
        public TechnicalSkillRepository(PortfolioDBContext context) : base(context)
        {
            _technicalSkills = context.TechnicalSkills;
        }
    }
}