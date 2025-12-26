using UnityEditor;
using UnityEngine;

public class ManagerSpawnerWindow : EditorWindow
{
    [MenuItem("Tools/Manager Spawner")]
    public static void ShowWindow()
    {
        GetWindow<ManagerSpawnerWindow>("Manager Spawner");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("모든 매니저 프리팹 스폰"))
        {
            SpawnAllPrefabs();
        }
    }

    private void SpawnAllPrefabs()
    {
        // 패키지 경로를 전체 경로로 지정
        string prefabFolder = "Packages/com.phikozz.phikozzlibrary/Prefabs";
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabFolder });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
            if (prefab != null)
            {
                PrefabUtility.InstantiatePrefab(prefab);
            }
        }
    }
}