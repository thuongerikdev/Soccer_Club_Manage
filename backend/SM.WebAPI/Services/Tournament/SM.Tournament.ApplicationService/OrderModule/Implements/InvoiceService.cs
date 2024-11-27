using Microsoft.Extensions.Logging;
using SM.Tournament.ApplicationService.Common;
using SM.Tournament.ApplicationService.OrderModule.Abtracts;
using SM.Tournament.Domain.Orders;
using SM.Tournament.Dtos;
using SM.Tournament.Dtos.OrderDto.OrderModel.Invoice;
using SM.Tournament.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.ApplicationService.OrderModule.Implements
{
    public class InvoiceService : TournamentServiceBase , IInvoiceService
    {
        public InvoiceService( ILogger<InvoiceService> logger , TournamentDbContext dbContext) : base(logger, dbContext)
        {
        }
        public async Task <TournamentResponeDto>  CreateInvoice(CreateInvoiceDto createInvoiceDto)
        {
            var invoice = new Invoices
            {
            
               InvoiceNumber = createInvoiceDto.InvoiceNumber,
                PaymentID = createInvoiceDto.PaymentID,
                PaymentMethod = createInvoiceDto.PaymentMethod,
                Status = createInvoiceDto.Status,
                CreatedDate = DateTime.Now,
                TransactionID = createInvoiceDto.TransactionID,
                
                
            };
             _dbContext.Invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorMessage = "Success",
                ErrorCode = 0,
                Data = invoice
            };
        }
        public async Task<TournamentResponeDto> UpdateInvoice(UpdateInvoiceDto updateInvoiceDto)
        {
            var invoice = await _dbContext.Invoices.FindAsync(updateInvoiceDto.InvoiceID);
            if (invoice == null)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = "Invoice not found",
                    ErrorCode = 1,
                    Data = null
                };
            }

            invoice.InvoiceNumber = updateInvoiceDto.InvoiceNumber;
            invoice.PaymentID = updateInvoiceDto.PaymentID;
            invoice.PaymentMethod = updateInvoiceDto.PaymentMethod;
            invoice.Status = updateInvoiceDto.Status;
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorMessage = "Success",
                ErrorCode = 0,
                Data = invoice
            };
        }
        public async Task<TournamentResponeDto> DeleteInvoice(int invoiceID)
        {
            var invoice = await _dbContext.Invoices.FindAsync(invoiceID);
            if (invoice == null)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = "Invoice not found",
                    ErrorCode = 1,
                    Data = null
                };
            }
            _dbContext.Invoices.Remove(invoice);
            await _dbContext.SaveChangesAsync();
            return new TournamentResponeDto
            {
                ErrorMessage = "Success",
                ErrorCode = 0,
                Data = invoice
            };
        }
        public async Task<TournamentResponeDto> GetInvoice(int invoiceID)
        {
            var invoice = await _dbContext.Invoices.FindAsync(invoiceID);
            if (invoice == null)
            {
                return new TournamentResponeDto
                {
                    ErrorMessage = "Invoice not found",
                    ErrorCode = 1,
                    Data = null
                };
            }
            return new TournamentResponeDto
            {
                ErrorMessage = "Success",
                ErrorCode = 0,
                Data = invoice
            };
        }
        public async Task<TournamentResponeDto> GetInvoices()
        {
            var invoices = _dbContext.Invoices.ToList();
            return new TournamentResponeDto
            {
                ErrorMessage = "Success",
                ErrorCode = 0,
                Data = invoices
            };
        }
        public async Task<TournamentResponeDto> SaveInvoice(SaveInvoiceDto saveInvoiceDto)
        {
            var invoice = new Invoices 
            {
                InvoiceNumber = saveInvoiceDto.InvoiceNumber,
                PaymentID = saveInvoiceDto.PaymentID,
                PaymentMethod = saveInvoiceDto.PaymentMethod,
                TransactionID = saveInvoiceDto.TransactionID,
                Status = true,
                CreatedDate = DateTime.Now,
            };
            await _dbContext.Invoices.AddAsync(invoice);
            await _dbContext.SaveChangesAsync();


            return new TournamentResponeDto
            {
                ErrorMessage = "Success",
                ErrorCode = 0,
                Data = null
            };
        }
    }
}
