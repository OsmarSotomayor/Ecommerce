using System.Linq.Expressions;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        //El nombre de esta clase es muy largo peron nos dice exacatmanete que esperamos de regreso
        public ProductsWithTypesAndBrandsSpecification(){
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
        }

        //Este nuevo constructor hereda y remplaza lo que tiene el constrolador
        // del Base specification

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=> x.Id == id)
        {
            this.AddInclude(x => x.ProductType);
            this.AddInclude(x => x.ProductBrand);
        }

    }
}