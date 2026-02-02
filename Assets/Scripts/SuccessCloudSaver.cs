using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessCloudSaver : MonoBehaviour
{
    [SerializeField] private string levelSelectSceneName = "LevelSelectScene";

    // זה מה שתחברי לכפתור
 public async void SaveThenGoToLevels()
{
    // אורח: לא שומרים לענן, רק פותחים שלב הבא בזיכרון
   if (GuestSession.IsGuest)
{
    int nextUnlock = Mathf.Clamp(GameState.CompletedLevelIndex + 1, 0, 3);
    GuestSession.UnlockedIndex = Mathf.Max(GuestSession.UnlockedIndex, nextUnlock);

    // ✅ כסף לאורח נשמר בזיכרון (במקום ענן)
    GuestSession.Money += GameState.Reward;

    SceneManager.LoadScene(levelSelectSceneName);
    return;
}

    // משתמש רגיל: ענן כרגיל
    try
    {
        await SaveNow();
    }
    catch (Exception e)
    {
        Debug.LogError("SaveThenGoToLevels failed: " + e.Message);
    }

    SceneManager.LoadScene(levelSelectSceneName);
}

    private async Task SaveNow()
    {
        var (unlocked, money) = await PlayerProgressCloud.LoadAsync();

        int newMoney = money + GameState.Reward;

        int nextUnlock = Mathf.Clamp(GameState.CompletedLevelIndex + 1, 0, 3);
        int newUnlocked = Mathf.Max(unlocked, nextUnlock);

        await PlayerProgressCloud.SaveAsync(newUnlocked, newMoney);

        Debug.Log($"Saved: unlocked={newUnlocked}, money={newMoney}, completed={GameState.CompletedLevelIndex}");
    }
}
