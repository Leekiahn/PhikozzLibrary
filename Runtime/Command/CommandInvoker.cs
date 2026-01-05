using System.Collections.Generic;

namespace PhikozzLibrary.Runtime.Command
{
    /// <summary>
    /// 커맨드 패턴에서 명령어를 실행하고 관리하는 인보커 클래스입니다.
    /// </summary>
    public class CommandInvoker
    {
        private readonly Stack<ICommand> _commandHistory = new Stack<ICommand>();

        /// <summary>
        /// 커맨드 실행
        /// </summary>
        /// <param name="command">실행할 커맨드</param>
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            _commandHistory.Push(command);
        }

        /// <summary>
        /// 커맨드 실행 취소
        /// </summary>
        public void UndoCommand()
        {
            if (_commandHistory.Count > 0)
            {
                var command = _commandHistory.Pop();
                command.Undo();
            }
        }

        /// <summary>
        /// 커맨드 히스토리 초기화
        /// </summary>
        public void ClearHistory()
        {
            _commandHistory.Clear();
        }
    }
}