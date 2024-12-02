using BP215Uniqlo.DataAcces;
using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Category;
using BP215Uniqlo.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController(UniqloDbContext _context) : Controller
    {


        public async Task<IActionResult> Index()
        {
            var data = await _context.Categories.ToListAsync();

            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (vm.Name == null) { ModelState.AddModelError("Name", " ad bos ola bilmez"); }
            if (ModelState.IsValid)
            {
                Category categories = new() { Name = vm.Name };
                await _context.Categories.AddAsync(categories);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Update(int? Id)
        {
            Category? data = await _context.Categories.FindAsync(Id);
            CategoryCreateVM vm = new();
            vm.Name = data.Name;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? Id , CategoryCreateVM vm)
        {
            if (!ModelState.IsValid) return BadRequest();
            var data = await _context.Categories.FindAsync(Id);
            if (data is null) return View();
            data.Name = vm.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Categories.Include(x => x.products).FirstOrDefaultAsync(x => x.Id == id);
            if (data is null) return BadRequest();
            _context.Categories.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));



        }

    }
}
