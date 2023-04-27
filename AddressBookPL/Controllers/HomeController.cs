
using AddressBookBL.EmailSenderBusiness;
using AddressBookBL.InterfacesOfManagers;
using AddressBookEL.IdentityModels;
using AddressBookEL.ViewModels;
using AddressBookPL.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AddressBookPL.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICityManager _cityManager;
        private readonly IDistrictManager _districtManager;
        private readonly INeighbourhoodManager _neighbourhoodManager;
        private readonly IUserAddressManager _userAddressManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailManager;

        public HomeController(ICityManager cityManager, IDistrictManager districtManager, INeighbourhoodManager neighbourhoodManager, IUserAddressManager userAddressManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IEmailSender emailManager)
        {
            _cityManager = cityManager;
            _districtManager = districtManager;
            _neighbourhoodManager = neighbourhoodManager;
            _userAddressManager = userAddressManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailManager = emailManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        //identity'yi kullandığımız için authorize için role eklenebilir
        [Authorize (Roles = "customer,guest")]
        public IActionResult AddAddress()
        {
            try
            {
                ViewBag.Cities = _cityManager.GetAll(x => !x.IsRemoved).Data;

                var user = _userManager.FindByNameAsync(HttpContext.User.Identity?.Name).Result;
                UserAddressVM model = new()
                {
                    UserId = user.Id
                };

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Cities = new List<CityVM>();
                return View();

            }
        }

     
        
        [HttpGet]
        public JsonResult GetCityDistricts(int id)
        {
            try
            {
                var data = _districtManager.GetAll(x => x.CityId == id && !x.IsRemoved).Data;
                if (data == null)
                {

                    return Json(new { issuccess = false, message = "ilceler buunamadi!" });
                }


                return Json(new { issuccess = true, message = "ilceler geldi", data });
            }
            catch (Exception ex)
            {

                return Json(new { issuccess = false, message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetDistrictNeighs(int id)
        {
            try
            {
                var data = _neighbourhoodManager.GetAll(x => x.DistrictId == id && !x.IsRemoved).Data;
                if (data == null)
                {

                    return Json(new { issuccess = false, message = "mahalleler buunamadi!" });
                }


                return Json(new { issuccess = true, message = "mahalleler geldi", data });
            }
            catch (Exception ex)
            {

                return Json(new { issuccess = false, message = ex.Message });
            }

        }

        [[HttpPost]
        [Authorize(Roles = "Customer")]
        public JsonResult SaveAddress([FromBody] UserAddressVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { issuccess = false, msg = "Verileri eksiksiz girdiğinize emin olun!" });
                }

                model.CreatedDate = DateTime.Now;

                // yeni gelen adres varsayılan mı? Evet ise veritabanındaki diğer varsayılanı KALDIR
                if (model.IsDefaultAddress)
                {
                    var prevDefault = _userAddressManager.GetByConditions(x => x.IsDefaultAddress
                    && !x.IsRemoved).Data;
                    if (prevDefault != null)
                    {
                        prevDefault.IsDefaultAddress = false;
                        _userAddressManager.Update(prevDefault);
                    }
                }

                var result = _userAddressManager.Add(model);
                if (result.IsSuccess)
                {
                    return Json(new { issuccess = true, msg = "Yeni adres eklendi!" });
                }
                return Json(new { issuccess = false, msg = "Ekleme BAŞARISIZ!" });

            }
            catch (Exception ex)
            {
                return Json(new { issuccess = false, msg = ex.Message });
            }
        }
}