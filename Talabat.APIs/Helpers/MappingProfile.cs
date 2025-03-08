using AutoMapper;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                 .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                 .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name));
        }
    }
}
