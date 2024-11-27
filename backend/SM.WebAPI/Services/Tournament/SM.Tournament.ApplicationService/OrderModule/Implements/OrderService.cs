using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Domain.Orders;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.OrderDto;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Implements
{
    public class OrderService : TournamentServiceBase, IOrderService
    {
        public OrderService(ILogger<OrderService> logger, TournamentDbContext dbContext) : base(logger, dbContext)
        {

        }
        public async Task<TournamentResponeDto> CreateOrder(CreateOrderDto createOrderDto)
        {
            try
            {
                var order = new Orders
                {
                    OrderDate = createOrderDto.OrderDate,
                    OrderStatus = createOrderDto.OrderStatus,
                    OrderAmount = createOrderDto.OrderAmount,
                    UserID = createOrderDto.UserID,
                    TournamentID = createOrderDto.TournamentID,
                    PaymentMethod = createOrderDto.PaymentMethod,
                    PaymentStatus = createOrderDto.PaymentStatus,
                    PaymentID = createOrderDto.PaymentID


                };
                _dbContext.Orders.Add(order);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Create Order Success",
                    Data = order.OrderID
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(updateOrderDto.OrderID);
                if (order == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Order not found",
                        Data = null
                    };
                }
                order.OrderDate = updateOrderDto.OrderDate;
                order.OrderStatus = updateOrderDto.OrderStatus;
                order.OrderAmount = updateOrderDto.OrderAmount;
                order.UserID = updateOrderDto.UserID;
                order.TournamentID = updateOrderDto.TournamentID;
                order.PaymentMethod = updateOrderDto.PaymentMethod;
                order.PaymentStatus = updateOrderDto.PaymentStatus;
                order.PaymentID = updateOrderDto.PaymentID;
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Update Order Success",
                    Data = order.OrderID
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> DeleteOrder(int orderID)
        {
            try
            {
                var order = _dbContext.Orders.FirstOrDefault(x => x.OrderID == orderID);
                if (order == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Order not found",
                        Data = null
                    };
                }
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Delete Order Success",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetOrder(int orderID)
        {
            try
            {
                var order = _dbContext.Orders.FirstOrDefault(x => x.OrderID == orderID);
                if (order == null)
                {
                    return new TournamentResponeDto
                    {
                        ErrorCode = 1,
                        ErrorMessage = "Order not found",
                        Data = null
                    };
                }
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Order Found",
                    Data = order
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = ex.Message,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> GetOrders()
        {
            try
            {
                var orders = _dbContext.Orders.ToList();
                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Order List",
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> ConfirmOrder(string PaymentID, int TournamentID, string PaymentMethod)
        {
            try
            {

                var existOrer = _dbContext.Orders.FirstOrDefault(x => x.TournamentID == TournamentID);
                existOrer.OrderStatus = "Confirmed";
                existOrer.PaymentStatus = "Confirmed";
                existOrer.PaymentID = PaymentID;
                existOrer.PaymentMethod = PaymentMethod;

                await _dbContext.SaveChangesAsync();

                return new TournamentResponeDto
                {
                    ErrorCode = 0,
                    ErrorMessage = "Order List",
                    Data = existOrer
                };
            }
            catch (Exception ex)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = ex.Message,
                    ErrorCode = 1,
                    Data = null
                };
            }
        }
        public async Task<TournamentResponeDto> getOrderByTour(int tournamentID)
        {
            var ord = _dbContext.Orders.FirstOrDefault(x => x.TournamentID == tournamentID);
            if (ord == null)
            {
                return new TournamentResponeDto
                {
                    ErrorCode = 1,
                    ErrorMessage = "Order not found",
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorCode = 0,
                ErrorMessage = "Get Order Success",
                Data = ord
            };
        }
    }
}
