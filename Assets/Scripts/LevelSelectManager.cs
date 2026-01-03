using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class LevelSelectManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    [Header("Checkmarks (✅)")]
    [SerializeField] private GameObject checkTutorial;
    [SerializeField] private GameObject checkEasy;
    [SerializeField] private GameObject checkMedium;
    [SerializeField] private GameObject checkHard;

    [Header("Money UI")]
    [SerializeField] private TextMeshProUGUI moneyText;

    // Cloud keys
    private const string KEY_UNLOCKED = "unlockedLevel"; // 0=tutorial only, 1=easy opened, 2=medium opened, 3=hard opened
    private const string KEY_MONEY = "money";            // int

    private async void Start()
    {
        await RefreshUI();
    }

    public async Task RefreshUI()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("Not signed in - cannot load Cloud Save. Returning with defaults.");
            ApplyUI(unlockedLevel: 0, money: 0);
            return;
        }

        await LoadAndApply();
    }

    private async Task LoadAndApply()
    {
        int unlockedLevel = 0;
        int money = 0;

        try
        {
            var keys = new HashSet<string> { KEY_UNLOCKED, KEY_MONEY };
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (data.TryGetValue(KEY_UNLOCKED, out var unlockedItem))
                unlockedLevel = unlockedItem.Value.GetAs<int>();

            if (data.TryGetValue(KEY_MONEY, out var moneyItem))
                money = moneyItem.Value.GetAs<int>();
        }
        catch (Exception e)
        {
            Debug.LogWarning("Cloud load failed: " + e.Message);
        }

        ApplyUI(unlockedLevel, money);
    }

    private void ApplyUI(int unlockedLevel, int money)
    {
        tutorialButton.interactable = true;
        easyButton.interactable     = unlockedLevel >= 1;
        mediumButton.interactable   = unlockedLevel >= 2;
        hardButton.interactable     = unlockedLevel >= 3;

        // ✅ סימון הצלחות:
        // unlockedLevel=1 => Tutorial הושלם
        // unlockedLevel=2 => Easy הושלם
        // unlockedLevel=3 => Medium הושלם
        if (checkTutorial) checkTutorial.SetActive(unlockedLevel >= 1);
        if (checkEasy)     checkEasy.SetActive(unlockedLevel >= 2);
        if (checkMedium)   checkMedium.SetActive(unlockedLevel >= 3);
        if (checkHard)     checkHard.SetActive(false);

        if (moneyText) moneyText.text = $"₪ {money}";
    }
}
