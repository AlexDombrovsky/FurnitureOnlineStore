using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FurnitureOnlineStore.Models.Products;
using Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Framework;

namespace FurnitureOnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService, IWebHostEnvironment webHostEnvironment,
            IPhotoService photoService, IMapper mapper)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
            _webHostEnvironment = webHostEnvironment;
            _photoService = photoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int? productCategoryId)
        {
            var models = await GetProducts(productCategoryId);

            foreach (var model in models) await GetProductCategories(model);
            return View(models);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductViewModel model = new();
            await GetProductCategories(model);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Create(model);

                if (model.UploadedPhotos != null)
                    foreach (var photo in model.UploadedPhotos)
                        model.Photos.Add(await _photoService.Create(photo, model.Id,
                            _webHostEnvironment.WebRootPath));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetById(id);
            var model = _mapper.Map<ProductViewModel>(product);
            await GetProductCategories(model);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(model);

                if (model.UploadedPhotos != null)
                    foreach (var photo in model.UploadedPhotos)
                        model.Photos.Add(await _photoService.Create(photo, model.Id,
                            _webHostEnvironment.WebRootPath));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateCategory()
        {
            ProductCategoryViewModel model = new();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _productCategoryService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetById(id);
            var model = _mapper.Map<ProductViewModel>(product);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetById(id);
            var result = await _productService.Delete(id);
            if (result)
            {
                foreach (var photo in product.Photos) await _photoService.Delete(photo.Id);

                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, WorkContext.ImagePath, product.Id.ToString());
                if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true);
            }

            return RedirectToAction("Index");
        }

        private async Task<List<ProductViewModel>> GetProducts(int? productCategoryId)
        {
            if (productCategoryId.HasValue)
            {
                var products = await _productService.GetByCategory(productCategoryId);
                return _mapper.Map<List<ProductViewModel>>(products);
            }
            else
            {
                var products = await _productService.GetAll();
                return _mapper.Map<List<ProductViewModel>>(products);
            }
        }

        private async Task GetProductCategories(ProductViewModel model)
        {
            var entities = await _productCategoryService.GetAll();
            model.AllCategories = entities.Select(_ => new SelectListItem {Text = _.Name, Value = _.Id.ToString()}).ToList();
        }
    }
}