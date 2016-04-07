using AutoMapper;
using AutoMapper.Mappers;
using MVC6.Seed.V1.Services.Identity;
using MVC6.Seed.V1.Services.Identity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Web.ViewModels.Registration
{
    public class RegisterViewModel
    {
        private static MappingEngine __mappingEngine;

        static RegisterViewModel()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<RegisterViewModel, RegistrationRequest>();
            __mappingEngine = mappingEngine;
        }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        public RegistrationRequest Map()
        {
            return __mappingEngine.Map<RegisterViewModel, RegistrationRequest>(this);
        }
    }
}
