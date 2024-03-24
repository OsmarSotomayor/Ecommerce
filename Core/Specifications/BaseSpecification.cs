using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria  {get; }

        //Los Includes, realizan los mismo que hacian antes en el product
        public List<Expression<Func<T, object>>> Includes {get; }
        = new List<Expression<Func<T, object>>>();
        
        //Metodo para agregar las expresiones
        protected void AddInclude(Expression<Func<T, object>> includeExpression){
            this.Includes.Add(includeExpression);
        }     
    }
}