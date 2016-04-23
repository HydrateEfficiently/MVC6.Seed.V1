using MVC6.Seed.V1.CodeGeneration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Framework.Models.Identity
{
    [GeneratedEntity(TableName = "UserRelationships")]
    public class ApplicationUserRelationships
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
