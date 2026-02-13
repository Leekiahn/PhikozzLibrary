using UnityEngine;

namespace PhikozzLibrary.Runtime.ServiceLocater
{
    /// <summary>
    /// 제네릭 싱글톤 베이스 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericSingleton<T> : MonoBehaviour where T : Component
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
                Debug.Log(typeof(T).Name + " 인스턴스가 생성되었습니다.");
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}