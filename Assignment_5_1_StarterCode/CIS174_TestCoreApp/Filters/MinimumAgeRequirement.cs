using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Filters
{
    public class MinimumAgeRequirement: IAuthorizationRequirement
    {
        public int MinimunAge { get; }
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimunAge = minimumAge;

        }
    }
}
