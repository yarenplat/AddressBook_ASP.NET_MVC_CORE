using AddressBookEL.IdentityModels;
using AddressBookEL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookDL
{
    public class MyContext :
        IdentityDbContext<AppUser, AppRole, string>
    {
        //Bir önceki projede DbContrxten kalıtım aldık. Bu projede ise Identity'ye ait tabloları kullanabilmek için IdentityDbContextten kalıtım aldık
        // IdentityDbContext 'in generic yapısını kullandık çünkü AppUser aracılığıyla microsoftun AspNetUsers tablosuna kendi istediğimiz kolonları ekledik

        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        {

        } // ctor

        public DbSet<City> CityTable { get; set; }
        public DbSet<District> DistrictTable { get; set; }
        public DbSet<Neighbourhood> NeighbourhoodTable { get; set; }
        public DbSet<UserAddress> UserAddressTable { get; set; }




    }
}
