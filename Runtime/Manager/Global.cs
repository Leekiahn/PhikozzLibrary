namespace PhikozzLibrary.Runtime.Manager
{
    /// <summary>
    /// 모든 매니저에 쉽게 접근할 수 있는 전역 클래스
    /// 각 매니저의 인스턴스는 internal static
    /// </summary>
    public static class Global
    {
        public static GameManager Game => GameManager.Instance;
        
        public static GameSceneManager Scene => GameSceneManager.Instance;
        
        public static SaveLoadManager SaveLoad => SaveLoadManager.Instance;
        
        public static InputManager Input => InputManager.Instance;
        
        public static UIManager UI => UIManager.Instance;
        
        public static BGMManager BGM => BGMManager.Instance;
        
        public static SFXManager SFX => SFXManager.Instance;
        
        public static ResourceManager Resource => ResourceManager.Instance;
    }
}
