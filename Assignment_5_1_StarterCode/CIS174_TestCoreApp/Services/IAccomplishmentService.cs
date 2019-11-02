using CIS174_TestCoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Services
{
    public  interface IAccomplishmentService  
    {
        IEnumerable<PersonAccomplishmentViewModel> GetAccomplishments();
        PersonDetailAccomplishmentViewModel GetAccomplisment(int id);
        bool Delete(int id);
    }
}
