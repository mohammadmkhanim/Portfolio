using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portfolio.Data.Repository.IRepositories;

namespace Portfolio.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IRecentWorkRepository RecentWorkRepository { get; }
        public IServiceRepository ServiceRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public IImageRepository ImageRepository { get; }
        public ITechnicalSkillRepository TechnicalSkillRepository { get; }
        public Task SaveAsync();
    }
}
