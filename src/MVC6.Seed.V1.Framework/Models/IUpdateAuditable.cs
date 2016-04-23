using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Models.Identity;

namespace MVC6.Seed.V1.Framework.Models
{
    public interface IUpdateAuditable
    {
        DateTime Updated { get; set; }

        string UpdatedById { get; set; }

        ApplicationUser UpdatedBy { get; set; }
    }

    public static class UpdateAuditableExtensions
    {
        public static T AuditUpdated<T>(
            this T updateAuditable,
            Audit audit)
            where T : IUpdateAuditable
        {
            updateAuditable.Updated = audit.Actioned;
            updateAuditable.UpdatedById = audit.ActionedById;
            return updateAuditable;
        }
    }
}
