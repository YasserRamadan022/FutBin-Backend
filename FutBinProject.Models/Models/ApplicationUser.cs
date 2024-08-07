﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FutBinProject.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public virtual LineUp LineUp { get; set; }
    }
}
