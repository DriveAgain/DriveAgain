using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.Authentication;
using UnityEngine;

[Serializable]
public class PlayerProgressData
{
    public int unlocked = 0; // 0 Tutorial, 1 Easy, 2 Medium, 3 Hard
    public bool tutorialDone = false;
    public bool easyDone = false;
    public bool mediumDone = false;
    public bool hardDone = false;
    public int money = 0;
}

public static class PlayerProgressService
{
    private const string KEY = "progress_v1";

    public static PlayerProgressData Cached { get; private set; } = new PlayerProgressData();

    public static async Task<PlayerProgressData> LoadAsync()
    {
        // חייב להיות SignedIn כדי לעבוד עם Cloud Save
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("LoadAsync: not signed in");
            Cached = new PlayerProgressData();
            return Cached;
        }

        try
        {
            var result = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string> { KEY });

            if (result.TryGetValue(KEY, out var item))
            {
                Cached = item.Value.GetAs<PlayerProgressData>();
            }
            else
            {
                Cached = new PlayerProgressData();
                await SaveAsync(Cached); // יוצרים נתון ראשוני בענן
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cloud load failed: " + e.Message);
            Cached = new PlayerProgressData();
        }

        return Cached;
    }

    public static async Task SaveAsync(PlayerProgressData data)
    {
        Cached = data;

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("SaveAsync: not signed in");
            return;
        }

        try
        {
            var dict = new Dictionary<string, object> { { KEY, data } };
            await CloudSaveService.Instance.Data.Player.SaveAsync(dict);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cloud save failed: " + e.Message);
        }
    }

    public static async Task MarkCompletedAndUnlockNext(string levelId)
    {
        var d = Cached ?? new PlayerProgressData();

        // מסמנים V + פותחים הבא
        switch (levelId)
        {
            case "Tutorial":
                d.tutorialDone = true;
                d.unlocked = Math.Max(d.unlocked, 1);
                break;
            case "Easy":
                d.easyDone = true;
                d.unlocked = Math.Max(d.unlocked, 2);
                break;
            case "Medium":
                d.mediumDone = true;
                d.unlocked = Math.Max(d.unlocked, 3);
                break;
            case "Hard":
                d.hardDone = true;
                break;
        }

        await SaveAsync(d);
    }

    public static async Task AddMoney(int amount)
    {
        var d = Cached ?? new PlayerProgressData();
        d.money += amount;
        await SaveAsync(d);
    }
}
