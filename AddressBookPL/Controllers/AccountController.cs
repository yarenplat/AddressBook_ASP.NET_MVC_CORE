using AddressBookEL.IdentityModels;
using AddressBookPL.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AddressBookPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                //ayni userName'den varsa hata versin
                var sameUser = _userManager.FindByNameAsync(model.UserName).Result; //async bir metodun sonuna .Result yazarsak method senkron calisir.
                if (sameUser != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı sistemde mevcuttur! Farklı kullanıcı adı deneyiniz.");
                }
                //ayni userName'den varsa hata versin.
                sameUser = _userManager.FindByEmailAsync(model.UserName).Result; //async bir metodun sonuna .Result yazarsak method senkron calisir.
                if (sameUser != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı sistemde mevcuttur! Farklı kullanıcı adı deneyiniz.");
                }
                //Artik sisteme kayit olabilir.
                AppUser user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    UserName = model.UserName,
                    CreatedDate = DateTime.Now,
                    EmailConfirmed = true, //dogrula yapmis kabul ettik.
                    IsPassive = false
                };
                if (model.Birthdate != null)
                {
                    user.BirthDate = model.Birthdate;
                }//user kaydedilsin
                var Result = _userManager.CreateAsync(user, model.Password).Result;
                if (Result.Succeeded)
                {
                    //Kullanici olusursa ona rol atamasi yapilacak!
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Ekleme başarısız!");
                   
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik hata oluştu!");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
