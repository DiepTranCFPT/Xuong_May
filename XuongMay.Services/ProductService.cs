using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuongMay.Models;
using XuongMay.Models.Entity;
using XuongMay.Services.IServices;

namespace XuongMay.Services
{
    public class ProductService : IProductService
    {
        private readonly XuongMayContext _context;

        public ProductService(XuongMayContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(updateProductDto.ProductID);

            if (product == null)
            {
                return false; // Không tìm thấy sản phẩm
            }

            product.CategoryID = updateProductDto.CategoryID;
            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Amount = updateProductDto.Amount;
            product.UnitPrice = updateProductDto.UnitPrice;
            product.Status = updateProductDto.Status;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
