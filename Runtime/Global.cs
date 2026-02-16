namespace PhikozzLibrary
{
    public static class Global 
    {
        public static IGameService GameState => ServiceLocator.Get<IGameService>();
        public static IAudioService Audio => ServiceLocator.Get<IAudioService>();
        public static IPoolService Pool => ServiceLocator.Get<IPoolService>();
        public static IEventService Event => ServiceLocator.Get<IEventService>();
        public static IResourceService Resource => ServiceLocator.Get<IResourceService>();
        public static ISaveService Save => ServiceLocator.Get<ISaveService>();
        public static IUIService UI => ServiceLocator.Get<IUIService>();
    }
}
