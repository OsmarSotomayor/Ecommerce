using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Core.SpecificationEvaluator
{
    public class SpecificationEvaluator<TEntity>
    where TEntity: BaseEntity
    {
        public static IQueryable<TEntity> getQuery(
        IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            //Se copia la consulta de entrada (inputQuery) en una variable llamada query. Esto se hace para no modificar la consulta original y trabajar con una copia
            var query = inputQuery;

            //Evaluamos la especificacion Se verifica si la especificación tiene criterios definidos (specification.Criteria). Si es así, se aplica el criterio a la consulta utilizando el método Where.
            if(specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            //Se aplican los includes especificados en la especificación a la consulta utilizando el método Include. Esto permite cargar entidades relacionadas con la entidad principal, evitando así la carga perezosa y mejorando el rendimiento de la consulta.
            query = specification.Includes.
            Aggregate(query, (current, include) => 
            current.Include(include));

            return query;
            //En resumen, esta clase SpecificationEvaluator proporciona un método genérico (getQuery) 
            //para aplicar especificaciones a consultas de Entity Framework Core, lo que permite una forma flexible y reutilizable de construir y ejecutar consultas basadas en ciertos criterios y relaciones de entidades.
            
        }
    }      
}