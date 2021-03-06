﻿using AutoMapper;
using AutoMapper.Mappers;
using MVC6.Seed.V1.Framework.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6.Seed.V1.Services.Identity.Models
{
    public class RegistrationRequest
    {

        private static MappingEngine __mappingEngine;

        static RegistrationRequest()
        {
            var configuration = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);
            var mappingEngine = new MappingEngine(configuration);
            configuration.CreateMap<RegistrationRequest, ApplicationUser>()
                .ForMember(dest => dest.UserName, opts => opts.ResolveUsing(src => src.Email));
            __mappingEngine = mappingEngine;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ApplicationUser ToApplicationUser()
        {
            return __mappingEngine.Map<RegistrationRequest, ApplicationUser>(this);
        }
    }
}
