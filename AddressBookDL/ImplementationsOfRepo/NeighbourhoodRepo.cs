using AddressBookEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookDL.ImplementationsOfRepo
{
    public class NeighbourhoodRepo:Repository<Neighbourhood,int>
    {
        public NeighbourhoodRepo(MyContext context):base(context)
        {
            
        }
    }
}
