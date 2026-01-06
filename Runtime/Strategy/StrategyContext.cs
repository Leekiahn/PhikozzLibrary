namespace PhikozzLibrary.Runtime.Strategy
{
    /// <summary>
    /// 전략 패턴에서 전략을 설정하고 실행하는 컨텍스트 클래스입니다.
    /// </summary>
    /// <typeparam name="T">전략이 작동할 컨텍스트 타입</typeparam>
    public class StrategyContext<T>
    {
        protected IStrategy<T> strategy;
    
        public void SetStrategy(IStrategy<T> strategy)
        {
            this.strategy = strategy;
        }
    
        public void ExecuteStrategy(T context)
        {
            strategy?.Execute(context);
        }
    }
}