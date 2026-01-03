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
        try
        {
            await SaveNow(); // קודם לשמור
        }
        catch (Exception e)
        {
            Debug.LogError("SaveThenGoToLevels failed: " + e.Message);
        }

        // ואז לעבור סצנה
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
