using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Models;
using MVC6.Seed.V1.Framework.Services;
using MVC6.Seed.V1.Framework.Utility;

namespace MVC6.Seed.V1.Services.Identity
{
    public interface IAuditFactory
    {
        Audit CreateAudit();
    }

    public class AuditFactory : IAuditFactory
    {
        private readonly IIdentityResolver _identityResolver;

        private Audit _audit;

        public AuditFactory(IIdentityResolver identityResolver)
        {
            _identityResolver = identityResolver;
        }

        public Audit CreateAudit()
        {
            if (_audit == null)
            {
                _audit = new Audit(
                    DateTime.UtcNow,
                    _identityResolver.Resolve().GetId());
            }
            return _audit;
        }
    }
}
