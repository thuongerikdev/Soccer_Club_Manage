using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Auth.Domain
{
    [Table(nameof(AuthRole))]
    public class AuthRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int roleId { get; set; }

        [MaxLength(50)]
        public string roleName { get; set; }

        [MaxLength(50)]
        public string description { get; set; }

    }
}
