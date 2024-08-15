using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Business.Models
{
    public class OrderDTO
    {
        [Key]
       public int? Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public List<OrderItemDTO> Items { get; set; }

        [ScaffoldColumn(false)]
        public decimal? TotalOrder { get; set; }
    }
}
