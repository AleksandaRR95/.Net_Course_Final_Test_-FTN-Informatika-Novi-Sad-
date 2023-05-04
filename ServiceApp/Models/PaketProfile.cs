using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class PaketProfile : Profile
    {
        public PaketProfile()
        {
            CreateMap<Paket, PaketDTO>();
        }
    }
}
