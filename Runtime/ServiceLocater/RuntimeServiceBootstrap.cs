using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public static class RuntimeServiceBootstrap
    {
        private const string ConfigResourcePath = "ManagerBootstrapConfig";
        private static bool _initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Bootstrap()
        {
            if (_initialized) return;
            _initialized = true;
            Initialize();
        }

        private static void Initialize()
        {
            ManagerBootstrapConfig config = Resources.Load<ManagerBootstrapConfig>(ConfigResourcePath);
            if (config == null)
            {
                Debug.LogError(
                    $"ManagerBootstrapConfig를 찾을 수 없습니다. " +
                    $"Resources/{ConfigResourcePath}.asset 경로에 배치되어 있는지 확인하세요.");
                return;
            }

            List<IInitializable> initializers = new List<IInitializable>();

            foreach (var prefab in config.ManagerPrefabs)
            {
                GameObject instance = Object.Instantiate(prefab);

                IInitializable[] foundInitializers = instance.GetComponentsInChildren<IInitializable>(true);
                initializers.AddRange(foundInitializers);
            }

            foreach (var initializer in initializers)
            {
                bool success = initializer.Init();
                Debug.Log(success
                    ? $"✅ {initializer.GetType().Name} 초기화 성공"
                    : $"❌ {initializer.GetType().Name} 초기화 실패");
            }
        }
    }
}