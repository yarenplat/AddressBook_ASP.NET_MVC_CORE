using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookEL.Models
{
    [Table("Cities")]
    public class City:BaseNumeric
    {
        [Required]
        [StringLength(50,MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string PlateCode { get; set; }

    }
}
