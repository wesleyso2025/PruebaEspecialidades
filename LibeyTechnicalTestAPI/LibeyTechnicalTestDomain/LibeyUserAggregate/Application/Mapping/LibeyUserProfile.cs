using AutoMapper;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Mapping
{
    public class LibeyUserProfile : Profile
    {
        public LibeyUserProfile()
        {
            // Mapeo de LibeyUser a LibeyUserResponse
            CreateMap<LibeyUser, LibeyUserResponse>();

        }
    }
}
