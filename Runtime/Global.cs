namespace PhikozzLibrary
{
    public static class Global 
    {
        public static IAudioService Audio => ServiceLocator.Get<IAudioService>();
    }
}
