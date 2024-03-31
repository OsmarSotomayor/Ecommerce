using AutoMapper;
using Core;

namespace API.Dtos.Helpers
{
    public class ProductUrlResolver : IValueResolver
    <Product, ProductsToReturnDTO, string>
    {
        private readonly IConfiguration configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductsToReturnDTO destination, 
        string destMember, ResolutionContext context)
        {
            //revisamos si la url esta vacia
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                //retornamos la url completa de la imagen
                return configuration["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}