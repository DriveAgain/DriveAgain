public static class GuestSession
{
    public static bool IsGuest = false;

    // 0 Tutorial, 1 Easy, 2 Medium, 3 Hard
    public static int UnlockedIndex = 0;
    public static int Money = 0;


    public static void StartGuest()
    {
        IsGuest = true;
        UnlockedIndex = 0;
        Money = 0;


        // איפוס נתונים זמניים של המשחק (אופציונלי אבל מומלץ)
        GameState.Stars = 3;
        GameState.Reward = 0;
        GameState.TotalSeconds = 0;
        GameState.CompletedLevelIndex = 0;
    }

    public static void EndGuest()
    {
        IsGuest = false;
        UnlockedIndex = 0;
    }
}
