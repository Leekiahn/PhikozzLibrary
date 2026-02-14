using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IPreinitialize 
{
    UniTask<T> PreinitializeAsync<T>() where T : MonoBehaviour;
}
