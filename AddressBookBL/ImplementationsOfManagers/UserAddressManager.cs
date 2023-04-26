using AddressBookBL.InterfacesOfManagers;
using AddressBookDL.InterfacesOfRepo;
using AddressBookEL.Models;
using AddressBookEL.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookBL.ImplementationsOfManagers
{
    public class UserAddressManager : Manager<UserAddressVM, UserAddress, int>,IUserAddressManager
    {

        public UserAddressManager(IUserAddressRepo repo, IMapper mapper)
            : base(repo, mapper, "NeighbourhoodFK,AppUser")
        {

        }
    }
}
