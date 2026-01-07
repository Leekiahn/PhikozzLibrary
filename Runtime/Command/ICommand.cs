namespace PhikozzLibrary.Runtime.Command
{
    /// <summary>
    /// 커맨드 패턴에서 사용되는 명령어 인터페이스입니다.
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
