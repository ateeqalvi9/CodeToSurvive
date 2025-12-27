using CodeToSurvive.DLL;

public static class GameState
{
    public static Player Player { get; private set; }

    public static void Initialize()
    {
        Player = new Player();
    }
}