using System.ComponentModel;
using Core;
using Core.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T: BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context){
            this.context = context;
        }

        public async Task<T> GetByIdAsyncId(int id)
        {
            //set nos permite indicar que la intancia que pasemos
            //en T sera con la que trabajara y donde buscara con 
            //FindAsync
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            //De igual form lo que pasemos como T sera usado en el método 
            
            return await context.Set<T>().ToListAsync();
        }
    }
}