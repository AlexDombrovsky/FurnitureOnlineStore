using System.Threading.Tasks;
using DomainModel.Products;
using Microsoft.AspNetCore.Http;

namespace Interfaces.Products
{
    public interface IPhotoService
    {
        Task<Photo> Create(IFormFile photo, int productId, string wwwroot);
        Task<bool> Delete(int id);
    }
}