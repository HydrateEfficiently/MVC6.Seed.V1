using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Framework.Services
{
    public interface IUrlResolver
    {
        string ResolveConfirmationEmailUrl(string userId, string emailConfirmationToken);
    }
}
