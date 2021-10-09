using System.Collections.Generic;
using DomainModel.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FurnitureOnlineStore.Models.Products
{
    public class ProductViewModel : Product
    {
        public List<SelectListItem> AllCategories { get; set; }
        public List<IFormFile> UploadedPhotos { get; set; }
    }
}