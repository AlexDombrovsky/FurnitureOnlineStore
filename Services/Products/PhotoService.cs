using System;
using System.IO;
using System.Threading.Tasks;
using DomainModel.Products;
using Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Framework;

namespace Services.Products
{
    public class PhotoService : IPhotoService
    {
        private readonly ApplicationDbContext _dbContext;

        public PhotoService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Photo> Create(IFormFile photo, int productId, string wwwroot)
        {
            var filePath =
                Directory.CreateDirectory(Path.Combine(wwwroot, WorkContext.ImagePath, productId.ToString()));
            if (!Directory.Exists(filePath.FullName)) Directory.CreateDirectory(filePath.FullName);

            var fileName = Path.Combine(WorkContext.ImagePath, productId.ToString(),
                Guid.NewGuid() + Path.GetExtension(photo.FileName));

            await using var fileSteam = new FileStream(Path.Combine(wwwroot, fileName), FileMode.Create);
            await photo.CopyToAsync(fileSteam);

            var entity = new Photo
            {
                Path = @"\" + fileName,
                ProductId = productId
            };
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Photos.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.Photos.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}