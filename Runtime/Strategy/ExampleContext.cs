using UnityEngine;
using PhikozzLibrary.Runtime.Strategy;

/// <summary>
/// 예제 전략 컨텍스트 클래스입니다.
/// </summary>
public class ExampleContext : MonoBehaviour
{
    private StrategyContext<GameObject> _context = new StrategyContext<GameObject>();
    
    public void SetStrategy(IStrategy<GameObject> strategy)
    {
        _context.SetStrategy(strategy);
    }
    
    public void ExecuteStrategy()
    {
        _context.ExecuteStrategy(this.gameObject);
    }
}
