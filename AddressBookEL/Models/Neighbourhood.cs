using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookEL.Models
{
    [Table("Neighbourhoods")]
    public class Neighbourhood:BaseNumeric
    {
        //eğer bu tablonun PK'sı string olsaydı basenumaricten(çünkü basenumericin id'si birer birer atan komut ile oluşturuldu) kalıtım alamazdı.
        //1)böyle bir senaryoda BaseNonNumeric classı oluşturulabilir.
        //2)Kalıtım almadan bu classın içi direkt prop tanımlanabilir.


        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 5)]//5'in altını ve üstünü kabul etmez
        public string PostCode { get; set; }
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
}
