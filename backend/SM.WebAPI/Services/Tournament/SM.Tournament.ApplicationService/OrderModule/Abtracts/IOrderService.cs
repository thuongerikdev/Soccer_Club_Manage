using SM.Tournament.Dtos;
using SM.Tournament.Dtos.OrderDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Abtracts
{
    public interface IOrderService
    {
        public Task<TournamentResponeDto> CreateOrder(CreateOrderDto createOrderDto);
        public Task<TournamentResponeDto> UpdateOrder(UpdateOrderDto updateOrderDto);
        public Task<TournamentResponeDto> DeleteOrder(int orderID);
        public Task<TournamentResponeDto> GetOrder(int orderID);
        public Task<TournamentResponeDto> GetOrders();
        public Task<TournamentResponeDto> ConfirmOrder(string OrderID , int tournamentID ,  string PaymentMethod);
        public Task<TournamentResponeDto> getOrderByTour(int tournamentID);
    }
}
