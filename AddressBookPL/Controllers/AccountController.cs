using AddressBookEL.IdentityModels;
using AddressBookPL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

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
                    return View(model);
                }

                // aynı username'den varsa hata versin
                var sameUser = _userManager.FindByNameAsync(model.UserName).Result; // async bir metodun sonuna .Result yazarsak metod senkron çalışır
                if (sameUser != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı ismi sistemde mevcuttur! Farklı kullanıcı adı deneyiniz!");
                }

                // aynı email'den varsa hata versin
                sameUser = _userManager.FindByEmailAsync(model.Email).Result; // async bir metodun sonuna .Result yazarsak metod senkron çalışır
                if (sameUser != null)
                {
                    ModelState.AddModelError("", "Bu email ile sistemde mevcuttur! Farklı email deneyiniz!");
                }
                // artık sisteme kayıt olabilir

                AppUser user = new AppUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone,
                    Email = model.Email,
                    UserName = model.UserName,
                    CreatedDate = DateTime.Now,
                    EmailConfirmed = true, // doğrula yapmış kabul ettik
                    IsPassive = false
                };
                if (model.Birthdate != null)
                    user.BirthDate = model.Birthdate;
                //user kaydedilsin
                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    // kullanıya customer rolünü atayalım

                    var roleResult = _userManager.AddToRoleAsync(user, "Customer").Result;
                    if (roleResult.Succeeded)
                    {
                        TempData["RegisterSuccessMsg"] = "Kayıt başarılı!";
                    }
                    else
                    {
                        TempData["RegisterWarningMsg"] = "Kullanıcı oluştu! Ancak rolü atanamadı! Sistem yöneticisine ulaşarak rol ataması yapılmalıdır!";
                    }
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Ekleme başarısız!");
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                    return View(model);
                }
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


        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = _userManager.FindByNameAsync(model.UsernameOrEmail).Result;
                if (user == null)
                {
                    user = _userManager.FindByEmailAsync(model.UsernameOrEmail).Result;
                }
                if (user == null)
                {
                    ModelState.AddModelError("", "Kullanıcı Adı/Email ya da şifreniz hatalıdır!");
                    return View(model);
                }
                // user'ı bulduk



                var signinResult =
                 _signInManager.PasswordSignInAsync(user, model.Password, true, true).Result;

                if (!signinResult.Succeeded)
                {
                    ModelState.AddModelError("", "Giriş başarısızdır!");
                    if (signinResult.IsLockedOut)
                    {
                        var r = _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow.AddMinutes(1)).Result;
                        var r2 = _userManager.UpdateAsync(user).Result;
                        ModelState.AddModelError("", $"2 defa yanlış işlem yaptığınız için {user.LockoutEnd.Value.ToString("HH:mm:ss")} den sonra giriş yapabilirsiniz! ");
                    }

                    return View(model);
                }

                // yönlendirme yapılacak
                if (_userManager.IsInRoleAsync(user, "Customer").Result)
                {
                    TempData["LoggedInUsername"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else if (_userManager.IsInRoleAsync(user, "Admin").Result)
                {
                    return RedirectToAction("Dashboard", "Admin", new { area = "" }); // areayı unutma!

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


                return View();


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                return View(model);

            }
        }



        [Authorize]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            TempData["LoggedInUsername"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}