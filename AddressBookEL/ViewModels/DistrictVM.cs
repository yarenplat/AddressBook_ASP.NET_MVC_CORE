using AddressBookEL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookEL.ViewModels
{
    public class DistrictVM//Data Transfer Object(class)
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRemoved { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        public int CityId { get; set; } 
        public CityVM City { get; set; }//UI katmanında ilçenin hangi ile bağlı olduğunu görebilmek veya gösterebbilmek için VM içine bu prop eklendi(join)
    }
}
