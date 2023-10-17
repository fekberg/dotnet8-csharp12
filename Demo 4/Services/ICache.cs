public interface ICache
{
    Task AddAsync<T>(T item);
    Task<T> GetAsync<T>();
}

public class DistributedCache : ICache
{
    public Task AddAsync<T>(T item)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>()
    {
        throw new NotImplementedException();
    }
}

public class InMemoryCache : ICache
{
    public Task AddAsync<T>(T item)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAsync<T>()
    {
        throw new NotImplementedException();
    }
}