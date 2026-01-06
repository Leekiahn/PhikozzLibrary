namespace PhikozzLibrary.Runtime.Strategy
{
    /// <summary>
    /// 전략 패턴에서 사용되는 전략 인터페이스입니다.
    /// </summary>
    /// <typeparam name="T">전략이 작동할 컨텍스트 타입</typeparam>
    public interface IStrategy<T>
    {
        void Execute(T context);
    }
}
