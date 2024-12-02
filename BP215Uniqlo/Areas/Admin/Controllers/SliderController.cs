using BP215Uniqlo.DataAcces;
using BP215Uniqlo.Models;
using BP215Uniqlo.ViewModels.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BP215Uniqlo.wwwroot.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController(UniqloDbContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            if (!vm.File.ContentType.StartsWith("image"))
                ModelState.AddModelError("File", "File type must be image");
            if (vm.File.Length > 600 * 1024)
                ModelState.AddModelError("File", "File length must be less than 600kb");
            if (!ModelState.IsValid) return View();

            string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.File.FileName);

            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "sliders", newFileName)))
            {
                await vm.File.CopyToAsync(stream);
            }

            Slider slider = new Slider
            {
                ImageURl = newFileName,
                Link = vm.Link,
                SubTitle = vm.Subtitle,
                Title = vm.Title,
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); 
        }
        public async Task<IActionResult> Update(int? id)
        {

            Slider? data = await _context.Sliders.FindAsync(id);
            SliderCreateVM vm = new();
            vm.Title = data.Title;
            vm.Subtitle = data.SubTitle;
            vm.Link = data.Link;
            
            return View(vm);
            
        }
        [HttpPost]
        public async Task<IActionResult> Update( int? id, SliderCreateVM vm)
        {
            if (!vm.File.ContentType.StartsWith("image"))
                ModelState.AddModelError("File", "File type must be image");
            if (vm.File.Length > 600 * 1024)
                ModelState.AddModelError("File", "File length must be less than 600kb");
            if (!ModelState.IsValid) return View();
            var data = await _context.Sliders.FindAsync(id);
            if (data is null) return View();

            string newFileName = Path.GetRandomFileName() + Path.GetExtension(vm.File.FileName);

            using (Stream stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, "imgs", "sliders", newFileName)))
            {
                await vm.File.CopyToAsync(stream);
            }

            
            data.Title = vm.Title;
            data.SubTitle = vm.Subtitle;
            data.Link = vm.Link;
            data.ImageURl = newFileName;           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data is null) return NotFound();
            _context.Sliders.Remove(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}