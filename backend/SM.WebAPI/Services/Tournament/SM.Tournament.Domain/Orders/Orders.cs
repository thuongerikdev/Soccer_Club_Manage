using SM.Constant.Database;
using SM.Tournament.Domain.Tournament;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SM.Tournament.Domain.Orders
{
    [Table(nameof(Orders), Schema = DbSchema.Tournament)]
    public  class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }
        public int UserID { get; set; }
        [MaxLength(50)]
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
