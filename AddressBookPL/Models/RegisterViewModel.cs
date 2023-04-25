using System.ComponentModel.DataAnnotations;


namespace AddressBookPL.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50,MinimumLength =2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        [Required]
        //Regular Expression yazılmalı
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 11)]
        //Regular Expression yazılmalı
        public string Phone { get; set; }
        [Required]
        public DateTime? Birthdate { get; set; }//null gelsin
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }

    }
}
