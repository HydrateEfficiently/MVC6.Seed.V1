using Microsoft.Data.Entity;
using MVC6.Seed.V1.Initializer.Initializers.Identity;
using MVC6.Seed.V1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Initializer.Initializers
{
    public class MainDataInitializer : IDataInitializer
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly SystemUserInitializer _systemUserInitializer;
        private readonly IEnvironmentInitializer _environmentInitializer;

        public MainDataInitializer(
            ApplicationDbContext dbContext,
            SystemUserInitializer systemUserInitializer,
            IEnvironmentInitializer environmentInitializer)
        {
            _dbContext = dbContext;
            _systemUserInitializer = systemUserInitializer;
            _environmentInitializer = environmentInitializer;
        }

        public void Run()
        {
            _dbContext.Database.Migrate();

            // Identity
            _systemUserInitializer.Run();

            _environmentInitializer.Run();
        }
    }
}
