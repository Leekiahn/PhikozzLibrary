using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using System.IO;

namespace PhikozzLibrary
{
    public class SaveManager : SingletonGlobal<SaveManager>, ISaveService, IPreinitialize
    {
        public UniTask<bool> InitAsync()
        {
            try
            {
                ServiceLocator.Register<ISaveService>(this);
                return UniTask.FromResult(true);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("서비스 초기화 실패: " + ex.Message);
                return UniTask.FromResult(false);
            }
        }

        private string GetSavePath(string key)
        {
            string company = Application.companyName;
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), company);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, key + ".json");
        }

        public void Save<T>(string key, T data)
        {
            try
            {
                // SaveData 타입일 때만 저장 시간 기록
                if (data is SaveData saveData)
                {
                    saveData.SaveTime = DateTime.Now.ToString("o"); // ISO 8601 형식
                }
                string json = JsonUtility.ToJson(data, true);
                string path = GetSavePath(key);
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"저장 실패: {ex.Message}");
            }
        }

        public T Load<T>(string key)
        {
            try
            {
                string path = GetSavePath(key);
                if (!File.Exists(path)) return default;
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"로드 실패: {ex.Message}");
                return default;
            }
        }
    }
}