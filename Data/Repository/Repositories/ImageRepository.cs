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
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private DbSet<Image> _images;
        public ImageRepository(PortfolioDBContext context) : base(context)
        {
            _images = context.Images;
        }

    }
}