using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        //Agregamos Generics methods 
        Expression<Func<T, bool>> Criteria{get;} //Agregaremos aqui el criterio
        //Traemos en lista un object que es lo mas generico que se puede crear en c#
        List<Expression<Func<T,object>>> Includes {get;}        
    }      
}