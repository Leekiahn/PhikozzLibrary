using PhikozzLibrary.Runtime.Strategy;
using UnityEngine;

/// <summary>
/// 예제 전략 클래스입니다.
/// </summary>
public class ExampleStrategy : IStrategy<GameObject>
{
    public void Execute(GameObject context)
    {
        Debug.Log($"ExampleStrategy executed on {context.name}");
    }
}
