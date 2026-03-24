using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class ShopManager : MonoBehaviour
{
    [Header("Money UI")]
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Buttons")]
    [SerializeField] private Button basicUseButton;       // אפור / חינם
    [SerializeField] private Button blueBuyButton;        // 300
    [SerializeField] private Button redBuyButton;         // 600
    [SerializeField] private Button yellowBuyButton;      // 900

    private const string KEY_MONEY = "money";
    private const string KEY_BOUGHT_BLUE = "boughtBlue";
    private const string KEY_BOUGHT_RED = "boughtRed";
    private const string KEY_BOUGHT_YELLOW = "boughtYellow";
    private const string KEY_SELECTED_CAR = "selectedCar";

    private int money = 0;
    private int boughtBlue = 0;
    private int boughtRed = 0;
    private int boughtYellow = 0;

    // 0 = gray, 1 = blue, 2 = red, 3 = yellow
    private int selectedCar = 0;

    private readonly Color selectedButtonColor = new Color(0.75f, 0.75f, 0.75f, 1f);
    private readonly Color normalButtonColor = Color.white;

    private async void Start()
    {
        await LoadShopData();
        await EnsureDefaultSelectedCarSaved();
        UpdateMoneyUI();
        UpdateButtonsUI();
    }

    private async Task LoadShopData()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
            await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.LogWarning("Not signed in");
            money = 0;
            selectedCar = 0; // ברירת מחדל = אפור
            return;
        }

        try
        {
            var keys = new HashSet<string>
            {
                KEY_MONEY,
                KEY_BOUGHT_BLUE,
                KEY_BOUGHT_RED,
                KEY_BOUGHT_YELLOW,
                KEY_SELECTED_CAR
            };

            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (data.TryGetValue(KEY_MONEY, out var moneyItem))
                money = moneyItem.Value.GetAs<int>();

            if (data.TryGetValue(KEY_BOUGHT_BLUE, out var blueItem))
                boughtBlue = blueItem.Value.GetAs<int>();

            if (data.TryGetValue(KEY_BOUGHT_RED, out var redItem))
                boughtRed = redItem.Value.GetAs<int>();

            if (data.TryGetValue(KEY_BOUGHT_YELLOW, out var yellowItem))
                boughtYellow = yellowItem.Value.GetAs<int>();

            if (data.TryGetValue(KEY_SELECTED_CAR, out var selectedItem))
                selectedCar = selectedItem.Value.GetAs<int>();
            else
                selectedCar = 0; // אם אין בחירה שמורה - אפור
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed loading shop data: " + e.Message);
            selectedCar = 0;
        }
    }

    private async Task EnsureDefaultSelectedCarSaved()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
            return;

        try
        {
            var keys = new HashSet<string> { KEY_SELECTED_CAR };
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (!data.ContainsKey(KEY_SELECTED_CAR))
            {
                await CloudSaveService.Instance.Data.Player.SaveAsync(
                    new Dictionary<string, object>
                    {
                        { KEY_SELECTED_CAR, 0 }
                    }
                );

                selectedCar = 0;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed ensuring default selected car: " + e.Message);
        }
    }

    private async Task SaveShopData()
    {
        try
        {
            var data = new Dictionary<string, object>
            {
                { KEY_MONEY, money },
                { KEY_BOUGHT_BLUE, boughtBlue },
                { KEY_BOUGHT_RED, boughtRed },
                { KEY_BOUGHT_YELLOW, boughtYellow }
            };

            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed saving shop data: " + e.Message);
        }
    }

    public async void UseBasicCar()
    {
        await UseCar(0);
    }

    public async void BuyBlueCar()
    {
        await BuyCar(300, blueBuyButton, "blue", 1);
    }

    public async void BuyRedCar()
    {
        await BuyCar(600, redBuyButton, "red", 2);
    }

    public async void BuyYellowCar()
    {
        await BuyCar(900, yellowBuyButton, "yellow", 3);
    }

    private async Task BuyCar(int price, Button button, string carKey, int carIndex)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        if (buttonText != null && buttonText.text == "USE")
        {
            await UseCar(carIndex);
            return;
        }

        if (money >= price)
        {
            money -= price;

            if (carKey == "blue")
                boughtBlue = 1;
            else if (carKey == "red")
                boughtRed = 1;
            else if (carKey == "yellow")
                boughtYellow = 1;

            await SaveShopData();
            UpdateMoneyUI();
            UpdateButtonsUI();

            Debug.Log("Bought car! Money left: " + money);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public async Task UseCar(int carIndex)
    {
        selectedCar = carIndex;

        try
        {
            var data = new Dictionary<string, object>
            {
                { KEY_SELECTED_CAR, carIndex }
            };

            await CloudSaveService.Instance.Data.Player.SaveAsync(data);
            Debug.Log("Selected car saved: " + carIndex);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed saving selected car: " + e.Message);
        }

        UpdateButtonsUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = $"₪ {money}";
    }

    private void UpdateButtonsUI()
    {
        // טקסטים
        SetButtonText(basicUseButton, "USE");
        SetBuyOrUseText(blueBuyButton, boughtBlue);
        SetBuyOrUseText(redBuyButton, boughtRed);
        SetBuyOrUseText(yellowBuyButton, boughtYellow);

        // הדגשת הרכב שנבחר
        HighlightSelectedButton(basicUseButton, selectedCar == 0);
        HighlightSelectedButton(blueBuyButton, selectedCar == 1);
        HighlightSelectedButton(redBuyButton, selectedCar == 2);
        HighlightSelectedButton(yellowBuyButton, selectedCar == 3);
    }

    private void SetBuyOrUseText(Button button, int isBought)
    {
        if (button == null) return;

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText == null) return;

        buttonText.text = (isBought == 1) ? "USE" : "BUY";
    }

    private void SetButtonText(Button button, string text)
    {
        if (button == null) return;

        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        if (buttonText == null) return;

        buttonText.text = text;
    }

    private void HighlightSelectedButton(Button button, bool isSelected)
    {
        if (button == null || button.image == null) return;

        button.image.color = isSelected ? selectedButtonColor : normalButtonColor;
    }
}