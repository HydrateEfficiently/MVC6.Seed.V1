using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6.Seed.V1.Framework.Models.Identity;

namespace MVC6.Seed.V1.Framework.Models
{
    public interface ICreateAuditable
    {
        DateTime Created { get; set; }

        string CreatedById { get; set; }

        ApplicationUser CreatedBy { get; set; }
    }

    public static class CreateAuditableExtensions
    {
        public static T AuditCreated<T>(
            this T createAuditable,
            Audit audit)
            where T : ICreateAuditable
        {
            createAuditable.Created = audit.Actioned;
            createAuditable.CreatedById = audit.ActionedById;
            return createAuditable;
        }
    }
}
