using UnityEditor;
using UnityEngine;
using System.IO;

public class ProjectSettingWindow : EditorWindow
{
    #region >--------------------------------------------- GUI

    [MenuItem("Tools/Project Setting Window")]
    public static void ShowWindow()
    {
        GetWindow<ProjectSettingWindow>("Project Setting");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Project Setup"))
        {
            SetProjectFolders();
        }
    }

    #endregion

    #region >--------------------------------------------- Set

    /// <summary>
    /// 프로젝트 폴더 설정
    /// </summary>
    private void SetProjectFolders()
    {
        string[] folders = {
            "01.Scenes",
            "02.Scripts",
            "03.Prefabs",
            "04.Art",
            "05.Animations",
            "06.Audio",
            "07.Plugins",
            "Resources"
        };
        foreach (var folder in folders)
        {
            string path = Path.Combine("Assets", folder);
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("Assets", folder);
            }
            // 폴더별 기본 파일 생성
            SetDefaultFileForFolder(path, folder);
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 각 폴더에 기본 파일 설정
    /// </summary>
    /// <param name="path"></param>
    /// <param name="folder"></param>
    private void SetDefaultFileForFolder(string path, string folder)
    {
        // 02.Scripts에는 Example.cs
        if (folder == "02.Scripts")
        {
            string scriptPath = Path.Combine(path, "Example.cs");
            if (!File.Exists(scriptPath))
            {
                File.WriteAllText(scriptPath,
                    "using UnityEngine;\n\npublic class Example : MonoBehaviour\n{\n    void Start() { }\n}\n");
            }
        }
        // 03.Prefabs에는 Example.prefab (빈 텍스트 파일로 대체)
        else if (folder == "03.Prefabs")
        {
            string prefabPath = Path.Combine(path, "Example.prefab");
            if (!File.Exists(prefabPath))
            {
                File.WriteAllText(prefabPath, "// Example prefab placeholder");
            }
        }
        // 나머지 폴더에는 .keep 파일
        else
        {
            string keepPath = Path.Combine(path, ".keep");
            if (!File.Exists(keepPath))
            {
                File.WriteAllText(keepPath, "keep");
            }
        }
    }

    #endregion
}