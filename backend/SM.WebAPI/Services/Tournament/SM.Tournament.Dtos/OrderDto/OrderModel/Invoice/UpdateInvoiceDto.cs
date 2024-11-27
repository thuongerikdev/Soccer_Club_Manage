using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.OrderDto.OrderModel.Invoice
{
    public class UpdateInvoiceDto
    {
        public int InvoiceID { get; set; }
        public string TransactionID { get; set; }
        public string InvoiceNumber { get; set; }
        public string PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
