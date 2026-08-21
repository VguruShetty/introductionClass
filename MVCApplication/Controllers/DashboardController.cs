using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApplication.Data;
using MVCApplication.Dto;

namespace MVCApplication.Controllers
{
    [Authorize]
    public class DashboardController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            var productList = _context.Products.Select(x => new ProductDTo { Id = x.Id, ProductName = x.ProductName,
                ProductDescription = x.ProductDescription,
                Price = x.Price,
                ProductColor = x.ProductColor
            }).ToList();
            return View(productList);
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        public async Task<IActionResult> CreatProduct(ProductDTo productDTo)
        {
            if(productDTo == null || !ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Kindly, Fill Products Details!!!.";
                return View("AddProduct");
            }

            var product = new Models.Product
            {
                ProductName = productDTo.ProductName,
                ProductDescription = productDTo.ProductDescription,
                Price = productDTo.Price,
                ProductColor = productDTo.ProductColor
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Product added successfully!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult UpdateProductForm(int id)
        {
            var data = _context.Products.Select(x => new ProductDTo { 
            Id = x.Id,
            ProductName = x.ProductName,
            ProductDescription = x.ProductDescription,
            ProductColor = x.ProductColor,
            Price = x.Price}).FirstOrDefault(x => x.Id == id);
            return View(data);
        }
        public async Task<IActionResult> UpdateProduct(ProductDTo pr)
        {
            if(pr == null || !ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Kindly, Fill Products Details!!!.";
                return View("UpdateProductForm", pr);
            }
            var product = _context.Products.FirstOrDefault(p => p.Id == pr.Id);
            if (product == null)
            {
                return NotFound();
            }
            product.ProductName = pr.ProductName;
            product.ProductDescription = pr.ProductDescription;
            product.Price = pr.Price;
            product.ProductColor = pr.ProductColor;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
