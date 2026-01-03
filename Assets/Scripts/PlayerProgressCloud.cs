using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public static class PlayerProgressCloud
{
    public const string KEY_UNLOCKED = "unlockedLevel"; // int: 0 tutorial, 1 easy, 2 medium, 3 hard
    public const string KEY_MONEY    = "money";         // int

    public static async Task EnsureReadyAsync()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
            throw new Exception("Not signed in - cannot use Cloud Save.");
    }

    public static async Task<(int unlockedLevel, int money)> LoadAsync()
    {
        await EnsureReadyAsync();

        int unlocked = 0;
        int money = 0;

        try
        {
            var keys = new HashSet<string> { KEY_UNLOCKED, KEY_MONEY };
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (data.TryGetValue(KEY_UNLOCKED, out var unlockedItem))
                unlocked = unlockedItem.Value.GetAs<int>();

            if (data.TryGetValue(KEY_MONEY, out var moneyItem))
                money = moneyItem.Value.GetAs<int>();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cloud Load failed: " + e.Message);
        }

        return (unlocked, money);
    }

    public static async Task SaveAsync(int unlockedLevel, int money)
    {
        await EnsureReadyAsync();

        var dict = new Dictionary<string, object>
        {
            { KEY_UNLOCKED, unlockedLevel },
            { KEY_MONEY, money }
        };

        try
        {
            await CloudSaveService.Instance.Data.Player.SaveAsync(dict);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cloud Save failed: " + e.Message);
        }
    }
}
