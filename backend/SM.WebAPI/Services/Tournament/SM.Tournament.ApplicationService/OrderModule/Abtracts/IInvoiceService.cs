using SM.Tournament.Dtos;
using SM.Tournament.Dtos.OrderDto.OrderModel.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Abtracts
{
    public interface IInvoiceService
    {
        public Task<TournamentResponeDto> CreateInvoice(CreateInvoiceDto createInvoiceDto);
        public Task<TournamentResponeDto> UpdateInvoice(UpdateInvoiceDto updateInvoiceDto);
        public Task<TournamentResponeDto> DeleteInvoice(int invoiceID);
        public Task<TournamentResponeDto> GetInvoice(int invoiceID);
        public Task<TournamentResponeDto> GetInvoices();
        public Task<TournamentResponeDto> SaveInvoice(SaveInvoiceDto saveInvoiceDto);
    }
}
