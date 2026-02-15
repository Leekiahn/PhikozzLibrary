using UnityEngine;

namespace PhikozzLibrary
{
    /// <summary>
    /// DontDestroyOnLoad 제네릭 싱글톤
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonGlobal<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        internal static T Instance
        {
            get
            {
                _instance = FindFirstObjectByType<T>();  // 씬에서 기존 인스턴스 찾기
                if (_instance == null)  // 없으면 새로 생성
                {
                    GameObject singleton = new GameObject();
                    singleton.name = typeof(T).Name;
                    _instance = singleton.AddComponent<T>();
                }
                return _instance;
            }
        }
        
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}