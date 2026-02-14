namespace PhikozzLibrary
{
    public static class Global 
    {
        public static IGameService GameState => ServiceLocator.Get<IGameService>();
        public static IAudioService Audio => ServiceLocator.Get<IAudioService>();
        public static IPoolService Pool => ServiceLocator.Get<IPoolService>();
    }
}
