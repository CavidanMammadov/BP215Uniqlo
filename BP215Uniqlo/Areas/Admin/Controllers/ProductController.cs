using BP215Uniqlo.DataAcces;
using BP215Uniqlo.Extensions;
using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Common;
using BP215Uniqlo.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController(IWebHostEnvironment _env, UniqloDbContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var data = await _context.Product.Include(x => x.category).ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
                return View();
            }
            if (vm.OtherImages != null && vm.OtherImages.Any())
            {
                if (!vm.OtherImages.All(x => x.IsValidType("image")))
                {
                    var fileNames = vm.OtherImages.Where(x => !x.IsValidType("image")).Select(x => x.FileName);
                    string.Join(",", fileNames);
                    ModelState.AddModelError("OtherImages", string.Join(",", fileNames) + "files must be an images");

                }
                if (!vm.OtherImages.All(x => x.IsValidSize(2 * 1024)))
                {
                    var fileNames = vm.OtherImages.Where(x => !x.IsValidSize(2 * 1024)).Select(x => x.FileName);
                    ModelState.AddModelError("OtherFiles", string.Join(",", fileNames) + "must be an image");
                }
            }

            if (!vm.CoverFile.IsValidType("image"))
                ModelState.AddModelError("CoverFile", "fayl image formatinda olmalidir");
            if (!vm.CoverFile.IsValidSize(3 * 1024))
                ModelState.AddModelError("CoverFile", "file must be less than 300 kb");

            string NewFileName = Path.GetRandomFileName() + Path.GetExtension(vm.CoverFile.FileName);
            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "sliders", NewFileName)))
            {
                await vm.CoverFile.CopyToAsync(stream);
            }

            Product product = new Product
            {
                CategoryId = vm.CategoryId,
                Name = vm.Name,
                Description = vm.Description,
                CostPrice = vm.CostPrice,
                SellPrice = vm.SellPrice,
                Quantity = vm.Discount,
                CoverImage = await vm.CoverFile!.UploadAsync(_env.WebRootPath, "imgs", "products"),
                Discount = vm.Discount
            };
            List<ProductImage> list = [];
            foreach (var item in vm.OtherImages)
            {
                string fileName = await item.UploadAsync(_env.WebRootPath, "imgs", "products");
                list.Add(new ProductImage
                {
                    FileUrl = fileName,
                    Product = product

                });

            }

            await _context.ProductImages.AddRangeAsync(list);
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int? Id)
        {
            if (!Id.HasValue) return BadRequest();
            var product = await _context.Product.Where(x => x.Id == Id.Value).Select(x => new ProductUpdateVM
            {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Description = x.Description,
                SellPrice = x.SellPrice,
                CostPrice = x.CostPrice,
                Discount = x.Discount,
                CoverFileUrl = x.CoverImage,
                Quantity = x.Quantity,
                OtherImagesUrls = x.Images.Select(y => new ImageUrlAndId
                {
                    Url = y.FileUrl,
                    Id = y.Id
                })

            }).FirstOrDefaultAsync();
            ViewBag.Categories = await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();

            return View(product);
        }
        public async Task<IActionResult> DeleteImage(int? Id)
        { if (!Id.HasValue) return BadRequest();
            var img = await _context.ProductImages.FindAsync(Id.Value);
            if (img == null) return NotFound();
            _context.ProductImages.Remove(img);
            await _context.SaveChangesAsync();
            return  Ok();
        }

        //public async Task<IActionResult> Update(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();

        //    var data = await _context.Product.FindAsync(id);
        //    if (data is null) return NotFound();
        //    ProductUpdateVM vm = new();
        //    vm.Name = data.Name;
        //    vm.CostPrice = data.CostPrice;
        //    vm.SellPrice = data.SellPrice;
        //    vm.Description = data.Description;
        //    vm.Quantity = data.Quantity;
        //    vm.Discount = data.Discount;
        //    return View(vm);
        //}
        [HttpPost]
        public async Task<IActionResult> Update(int? id, ProductUpdateVM vm)
        {
            if (!id.HasValue) return BadRequest();

            if (!vm.CoverFile.ContentType.StartsWith("image"))
                ModelState.AddModelError("CoverFile", "File must be an image");
            if (vm.CoverFile.Length > 600 * 1024)
                ModelState.AddModelError("CoverFile", "Image must be less than 600 kb");


            if (!ModelState.IsValid) return BadRequest();
            var data = await _context.Product.FindAsync(id);
            if (data == null) return View();


            string NewFileName = await vm.CoverFile.UploadAsync("wwwroot", "imgs", "products");

            data.Name = vm.Name;
            data.Description = vm.Description;
            data.SellPrice = vm.SellPrice;
            data.CostPrice = vm.CostPrice;
            data.Quantity = vm.Quantity;
            data.Discount = vm.Discount;
            data.CategoryId = vm.CategoryId;
            data.CoverImage = NewFileName;


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Product.FindAsync(id);
            if (data is null) return NotFound();
            _context.Product.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
