using SM.Constant.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Orders
{
    [Table(nameof(Invoices), Schema = DbSchema.Tournament)]
    public  class Invoices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceID { get; set; }
        public string TransactionID { get; set; }
        public string InvoiceNumber { get; set; }
        public string PaymentID { get; set; }
        public string PaymentMethod { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
