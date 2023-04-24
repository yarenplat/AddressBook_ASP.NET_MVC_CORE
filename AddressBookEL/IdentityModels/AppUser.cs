using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookEL.IdentityModels
{
    public class AppUser:IdentityUser
    {
        //IdentityUser classıın içindeki propertilerden farklı eklemek istediğimi özellikler varsa IdentityUser classsından kalıtım alarak tabloyu genişletebilriz.

        public DateTime CreatedDate { get; set; }

        [Required]
        [StringLength(50,MinimumLength =2)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsPassive { get; set; }

        //555 555 55 55
        [Required]
        [RegularExpression("^[0-9]*",ErrorMessage ="Telefon rakamlardan oluşmalıdır")]
        public override string PhoneNumber { get; set; }

        

    }
}
