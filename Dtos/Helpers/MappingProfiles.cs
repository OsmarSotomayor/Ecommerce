using API.Dtos.Helpers;
using AutoMapper;
using Core;

namespace API.Dtos
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            //Mapeamos de la entidad a el DTO
            //se mapearan las propiedades de Product en 
            // ProductsToReturnDTO
            CreateMap<Product, ProductsToReturnDTO>()
            .ForMember(d => d.ProductBrand, f => 
            f.MapFrom(s => s.ProductBrand.Name))
            .ForMember(d => d.ProductType, f => 
            f.MapFrom(s => s.ProductType.Name))
            .ForMember(destination => destination.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
             
        }
    }
}