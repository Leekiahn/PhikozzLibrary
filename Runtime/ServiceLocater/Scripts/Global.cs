namespace PhikozzLibrary
{
    public static class Global 
    {
        public static IAudioService Audio => ServiceLocator.Get<IAudioService>();
        
        public static IGameService Game => ServiceLocator.Get<IGameService>();
    }
}
