using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;

public class CarModelSelector : MonoBehaviour
{
    [SerializeField] private GameObject grayModel;    // 0 = Basic / Free
    [SerializeField] private GameObject blueModel;    // 1 = Blue
    [SerializeField] private GameObject redModel;     // 2 = Red
    [SerializeField] private GameObject yellowModel;  // 3 = Yellow

    private const string KEY_SELECTED_CAR = "selectedCar";

    private async void Start()
    {
        await LoadSelectedCar();
    }

    private async Task LoadSelectedCar()
    {
        int selectedCar = 0; // default = gray

        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
                await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                ApplySelectedCar(selectedCar);
                return;
            }

            var keys = new HashSet<string> { KEY_SELECTED_CAR };
            var data = await CloudSaveService.Instance.Data.Player.LoadAsync(keys);

            if (data.TryGetValue(KEY_SELECTED_CAR, out var item))
            {
                selectedCar = item.Value.GetAs<int>();
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to load selected car: " + e.Message);
        }

        ApplySelectedCar(selectedCar);
    }

    private void ApplySelectedCar(int selectedCar)
    {
        if (grayModel != null) grayModel.SetActive(false);
        if (blueModel != null) blueModel.SetActive(false);
        if (redModel != null) redModel.SetActive(false);
        if (yellowModel != null) yellowModel.SetActive(false);

        switch (selectedCar)
        {
            case 0:
                if (grayModel != null) grayModel.SetActive(true);
                break;

            case 1:
                if (blueModel != null) blueModel.SetActive(true);
                break;

            case 2:
                if (redModel != null) redModel.SetActive(true);
                break;

            case 3:
                if (yellowModel != null) yellowModel.SetActive(true);
                break;

            default:
                if (grayModel != null) grayModel.SetActive(true);
                break;
        }

        Debug.Log("Selected car loaded: " + selectedCar);
    }
}