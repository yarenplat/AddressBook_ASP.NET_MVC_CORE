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
    public class NeighbourhoodManager : Manager<NeighbourhoodVM, Neighbourhood, int>,INeighbourhoodManager
    {

        public NeighbourhoodManager(INeighbourhoodRepo repo, IMapper mapper)
            : base(repo, mapper, "District")
        {

        }
    }
}
