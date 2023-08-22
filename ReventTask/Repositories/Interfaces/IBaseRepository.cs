namespace ReventTask.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> AddAsync(T entity);
        Task<bool> Exists(int id);
        Task<T> UpdateAsync(T entity);

    }
}
