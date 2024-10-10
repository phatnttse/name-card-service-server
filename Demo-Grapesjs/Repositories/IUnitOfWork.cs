namespace Demo_Grapesjs.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task SaveChangesAsync();
    }
}
