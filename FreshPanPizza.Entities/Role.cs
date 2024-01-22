using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FreshPanPizza.Entities
{
    public class Role : IdentityRole<int>
    {
        // TO DO:
        public string Description { get; set; }
    }
}
