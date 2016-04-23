using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Models.Identity;

namespace MVC6.Seed.V1.Framework.Models
{
    public interface IDeleteAuditable
    {
        DateTime? Deleted { get; set; }

        string DeletedById { get; set; }

        ApplicationUser DeletedBy { get; set; }
    }

    public static class DeleteAuditable
    {
        public static T AuditDeleted<T>(
            this T deleteAuditable,
            Audit audit)
            where T : IDeleteAuditable
        {
            deleteAuditable.Deleted = audit.Actioned;
            deleteAuditable.DeletedById = audit.ActionedById;
            return deleteAuditable;
        }
    }
}
