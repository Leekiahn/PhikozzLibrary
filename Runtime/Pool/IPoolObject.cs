namespace PhikozzLibrary
{
    public interface IPoolObject
    {
        void OnCreate();
        void OnGet();
        void OnRelease();
        void OnDestroy();
    }
}
