namespace  PhikozzLibrary.Runtime.FSM
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}

