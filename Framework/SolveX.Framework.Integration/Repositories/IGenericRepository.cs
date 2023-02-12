namespace SolveX.Framework.Integration.Repositories;
public interface IGenericRepository<T>
{
    public Task<T> GetAsync(int id);

    public IQueryable<T> Query();

    public Task<int> InsertAsync(T entity);

    public Task UpdateAsync(T entity);

    public Task DeleteAsync(int id);
}