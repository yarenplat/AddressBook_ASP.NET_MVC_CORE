
using AddressBookBL.EmailSenderBusiness;
using AddressBookBL.InterfacesOfManagers;
using AddressBookEL.IdentityModels;
using AddressBookEL.ViewModels;
using AddressBookPL.Models;
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
        [Authorize]
        public IActionResult AddAddress()
        {
            try
            {
                ViewBag.Cities = _cityManager.GetAll(x => !x.IsRemoved).Data;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Cities = new List<CityVM>();
                return View();

            }
        }

    }
}