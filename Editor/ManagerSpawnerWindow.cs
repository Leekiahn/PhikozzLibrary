using UnityEditor;
using UnityEngine;
using System.Linq;

public class ManagerSpawnerWindow : EditorWindow
{
    private GameObject[] foundPrefabs;

    [MenuItem("Tools/Manager Spawner")]
    public static void ShowWindow()
    {
        GetWindow<ManagerSpawnerWindow>("Manager Spawner");
    }

    private void OnEnable()
    {
        LoadPrefabs();
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefabs 폴더 내 매니저 프리팹 자동 탐색", EditorStyles.boldLabel);

        if (foundPrefabs == null || foundPrefabs.Length == 0)
        {
            GUILayout.Label("프리팹이 없습니다.");
        }
        else
        {
            foreach (var prefab in foundPrefabs)
            {
                GUILayout.Label(prefab.name);
            }

            if (GUILayout.Button("하이어라키에 모두 생성"))
            {
                CreateManagers();
            }
        }

        if (GUILayout.Button("프리팹 다시 불러오기"))
        {
            LoadPrefabs();
        }
    }

    private void LoadPrefabs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { "Package/PhikozzLibrary/Prefabs" });
        foundPrefabs = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(go => go != null)
            .ToArray();
    }

    private void CreateManagers()
    {
        foreach (var prefab in foundPrefabs)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            Undo.RegisterCreatedObjectUndo(instance, "Create Manager Prefab");
        }
    }
}