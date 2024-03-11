namespace Core.interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<T> GetByIdAsyncId(int id);

        Task<IReadOnlyList<T>> ListAllAsync();
   
    } 
}
