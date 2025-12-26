using UnityEditor;
using UnityEngine;
using System.IO;

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
        string prefabFolder = "Packages/com.phikozz.phikozzlibrary/Prefabs";
        string tempFolder = "Assets/TempSpawnedPrefabs";

        if (!AssetDatabase.IsValidFolder(tempFolder))
            AssetDatabase.CreateFolder("Assets", "Scripts/Managers");

        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabFolder });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            string fileName = Path.GetFileName(assetPath);
            string destPath = Path.Combine(tempFolder, fileName).Replace("\\", "/");

            AssetDatabase.CopyAsset(assetPath, destPath);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(destPath);
            if (prefab != null)
            {
                PrefabUtility.InstantiatePrefab(prefab);
            }
        }

        AssetDatabase.Refresh();
    }
}