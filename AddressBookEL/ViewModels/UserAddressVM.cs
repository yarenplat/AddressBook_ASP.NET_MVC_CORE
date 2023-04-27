using AddressBookEL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressBookEL.IdentityModels;

namespace AddressBookEL.ViewModels
{
    public class UserAddressVM
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRemoved { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Details { get; set; }

        public string UserId { get; set; }
        public int NeighbourhoodId { get; set; }
        public bool IsDefaultAddress { get; set; }
        public NeighbourhoodVM?  NeighbourhoodFK { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
