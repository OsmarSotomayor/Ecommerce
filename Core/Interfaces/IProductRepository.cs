namespace Core.interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(int id);

        //IReadOnlyList es un tipo de lista mas especifica pues
        // solo nos permitira leer el contenido
    } 
}
