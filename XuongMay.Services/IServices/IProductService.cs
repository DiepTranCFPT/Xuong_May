using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XuongMay.Models;

namespace XuongMay.Services.IServices
{
    public interface IProductService
    {
        Task<bool> UpdateProductAsync(UpdateProductDto updateProductDto);
    }
}
