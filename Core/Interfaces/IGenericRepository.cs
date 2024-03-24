using Core.Specifications;

namespace Core.interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsyncId(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        
        //Metodos con especificacion
        Task<T> GetEntityWithSpec(ISpecification<T> specification);

        Task<IReadOnlyList<T>> listAsync(ISpecification<T> specification);       
    } 
}
