namespace Demo_Grapesjs.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task<T> UpdateAsync(T entity);
        IQueryable<T> GetQueryable();
        Task<int> CountAsync();
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    }
}
