
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
            CreateMap<Product, ProductsToReturnDTO>();
            
        }

    }

}