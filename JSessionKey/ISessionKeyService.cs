namespace JSessionKey
{
    public interface ISessionKeyService<T>
    {
        ISessionObject<T> CreateSession();
        ISessionKeyInfo<T> GetSessionKeyInfo(string key);
    }
}