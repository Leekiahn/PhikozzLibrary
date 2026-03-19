using UnityEngine;

namespace PhikozzLibrary
{
    /// <summary>
    /// 씬 단위 제네릭 싱글톤
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonScene<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        internal static T Instance { get { return _instance; } }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}