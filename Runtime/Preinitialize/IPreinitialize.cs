using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IPreinitialize
{
    UniTask<bool> InitAsync();
}
