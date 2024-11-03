using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public string Descricao { get; set; }
        public bool Status { get; set; }
    }
}
