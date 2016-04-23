using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Services;
using MVC6.Seed.V1.Framework.Utility;

namespace MVC6.Seed.V1.Framework.Models
{
    public class Audit
    {
        public DateTime Actioned { get; set; }

        public string ActionedById { get; set; }

        public Audit(DateTime actioned, string actionedId)
        {
            Actioned = actioned;
            ActionedById = actionedId;
        }
    }
}
