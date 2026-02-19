using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PhikozzLibrary
{
    public interface IPreinitialize
    {
        UniTask<bool> InitAsync();
    }
}
