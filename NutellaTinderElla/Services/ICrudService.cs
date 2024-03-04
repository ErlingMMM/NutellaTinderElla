using WebMovieApi.Data.Exceptions;

namespace WebMovieApi.Services
{
    //Interface that represents the basic CRUD (Create, Read, Update, Delete)
    //operations for a data entity in a software application.
    public interface ICrudService<TEntity, TID>
    {
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TID id);
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
        Task DeleteByIdAsync(TID id);
    }
}
