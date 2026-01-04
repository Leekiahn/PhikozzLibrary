namespace PhikozzLibrary.Runtime.Pooling
{
    public interface IObjectPool<T>
    {
        T Get();
        void Release(T obj);
        void Clear();
    }
}