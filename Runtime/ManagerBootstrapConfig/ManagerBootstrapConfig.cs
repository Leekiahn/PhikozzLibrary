using UnityEngine;

namespace PhikozzLibrary
{
    [CreateAssetMenu(
        fileName = "ManagerBootstrapConfig",
        menuName = "PhikozzLibrary/Manager Bootstrap Config")]
    public class ManagerBootstrapConfig : ScriptableObject
    {
        [SerializeField] private GameObject[] managerPrefabs;
        public GameObject[] ManagerPrefabs => managerPrefabs;
    }
}