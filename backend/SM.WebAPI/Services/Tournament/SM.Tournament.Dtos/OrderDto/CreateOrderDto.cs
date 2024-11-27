using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Dtos.OrderDto
{
    public class CreateOrderDto
    {
        public int UserID { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderAmount { get; set; }
        [MaxLength(50)]
        public string PaymentStatus { get; set; }
        [MaxLength(50)]
        public string PaymentMethod { get; set; }
        public int TournamentID { get; set; }
        public string PaymentID { get; set; }
    }
}
