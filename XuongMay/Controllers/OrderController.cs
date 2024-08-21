using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using XuongMay.Models;
using XuongMay.Models.Entity;

namespace XuongMay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly XuongMayContext _dbContext;
        public OrderController(XuongMayContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrder(int pageNumber = 1, int pageSize = 10)
        {
            var order = await _dbContext.Orders.Skip((pageNumber - 1) * pageSize)
                                   .Take(pageSize).ToListAsync();
            if (order == null) { return NotFound("Don't have any product"); }
            return Ok(order);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderDetails)
                                               .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] OrderRequestDto orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest("Order request is null");
            }

            // Tạo một đối tượng Order từ OrderRequestDTO
            var order = new Order
            {
                OrderDate = orderRequest.OrderDate,
                Amount = orderRequest.Amount,
                CustomerName = orderRequest.CustomerName,
                CustomerPhone = orderRequest.CustomerPhone,
                Status = false,
                OrderDetails = new List<OrderDetails>()
            };

            // Thêm chi tiết đơn hàng
            foreach (var detail in orderRequest.OrderDetails)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = detail.ProductId,
                    Amount = detail.Amount,
                    UnitPrice = detail.UnitPrice,
                    Order = order
                };
                order.OrderDetails.Add(orderDetail);
            }

            // Lưu đơn hàng và chi tiết đơn hàng vào database
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, UpdateOrderDto orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest("Order request is null");
            }

            // Tìm đơn hàng theo ID
            var existingOrder = await _dbContext.Orders.Include(o => o.OrderDetails)
                                                       .FirstOrDefaultAsync(o => o.OrderId == id);

            if (existingOrder == null)
            {
                return NotFound("Order not found");
            }

            // Cập nhật thông tin đơn hàng
            existingOrder.OrderDate = orderRequest.OrderDate;
            existingOrder.Amount = orderRequest.Amount;
            existingOrder.CustomerName = orderRequest.CustomerName;
            existingOrder.CustomerPhone = orderRequest.CustomerPhone;
            existingOrder.Status = orderRequest.Status;

            // Xóa các chi tiết đơn hàng cũ
            _dbContext.OrderDetails.RemoveRange(existingOrder.OrderDetails);

            // Thêm các chi tiết đơn hàng mới
            existingOrder.OrderDetails = new List<OrderDetails>();
            foreach (var detail in orderRequest.OrderDetails)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = detail.ProductId,
                    Amount = detail.Amount,
                    UnitPrice = detail.UnitPrice,
                    OrderId = existingOrder.OrderId // Liên kết với đơn hàng hiện tại
                };
                existingOrder.OrderDetails.Add(orderDetail);
            }

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _dbContext.SaveChangesAsync();

            return NoContent(); // Trả về 204 No Content nếu thành công
        }

    }
}
