using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookEL.Models
{
    [Table("Districts")]
    public class District:BaseNumeric
    {
        

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        public int CityId { get; set; }

        [ForeignKey("CityId")]//CityId'ye yzadığımız int değerinin city tablosunda karşılığı olmak zorunda
        //requirede yazmadık çünkü zaten fk gereklididr. Uygulama patlar.
        public virtual City City { get; set; }//CityId propertysi Foreign Key olacağı için burada city tablosuyla ilikisi kuruldu

        //DİPNOT: ilişkiler burada kuraulaceı gibi MYCONTEXT classı içindeki OnModelCreating metodu exilerek (override) kurulabilir.
    }
}
