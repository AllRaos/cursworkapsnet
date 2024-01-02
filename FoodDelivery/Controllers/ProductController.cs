using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.Data;
using FoodDelivery.Models;
using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var viewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ProductViewModel();
            return View(vm);
        }

        [HttpPost]
            public async Task<IActionResult> Create(ProductViewModel vm, IFormFile file)
            {
                var model = new Product
                {
                    Name = vm.Name,
                    Price = vm.Price,
                    Category = vm.Category,
                    Netto = vm.Netto,
                    Status = vm.Status,
                    // Map other properties as needed
                };

            if (file != null && file.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", file.FileName);

                try
                {
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    model.Image = "/Images/" + file.FileName;
                }
                catch (Exception ex)
                {
                    // Log the exception or print a message for debugging
                    Console.WriteLine($"Error saving image: {ex.Message}");
                }
            }

            _context.Products.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

        // Existing code...

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel vm, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = await _context.Products.FindAsync(vm.ProductId);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = vm.Name;
                existingProduct.Price = vm.Price;
                existingProduct.Category = vm.Category;
                existingProduct.Netto = vm.Netto;
                existingProduct.Status = vm.Status;

                if (file != null && file.Length > 0)
                {
                    // Delete the existing image if any
                    if (!string.IsNullOrEmpty(existingProduct.Image))
                    {
                        var existingImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", Path.GetFileName(existingProduct.Image));
                        System.IO.File.Delete(existingImagePath);
                    }

                    // Save the uploaded file to wwwroot/Images folder
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", file.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    existingProduct.Image = "/Images/" + file.FileName; // Save the relative path to the database
                }

                _context.Update(existingProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed   
            };

            return View(viewModel);
        }

    }
}