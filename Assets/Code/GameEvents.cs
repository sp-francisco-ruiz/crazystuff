
public static class GameEvents
{
    public class GameEvent
    {
    }

    public class ExitPlatformEvent : GameEvent
    {
        public PlatformController Platform;
        public ExitPlatformEvent(PlatformController platform)
        {
            Platform = platform;    
        } 
    }
}
