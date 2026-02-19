namespace  PhikozzLibrary
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}

