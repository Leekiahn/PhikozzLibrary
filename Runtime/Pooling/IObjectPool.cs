namespace PhikozzLibrary.Runtime.Pooling
{
    public interface IObjectPool<T>
    {
        T Get();
        void Set(T obj);
        void Clear();
    }
}