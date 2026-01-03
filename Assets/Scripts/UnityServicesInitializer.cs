using UnityEngine;
using Unity.Services.Core;
using System.Threading.Tasks;

public class UnityServicesInitializer : MonoBehaviour
{
    async void Awake()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized");
        }
    }
}
