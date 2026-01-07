
using UnityEngine;

public class inpotMessHard : MonoBehaviour
{
    [SerializeField] private PopupManager popup;

    void Awake()
    {
        if (popup == null)
            popup = FindFirstObjectByType<PopupManager>();
    }

    void Start()
    {
        if (popup == null)
        {
            Debug.LogError("IntroPopupManagerEasy: PopupManager not found!");
            return;
        }

        popup.ShowMessages(new string[]
        {
            "שים לב לחפצים בכביש "
        });
    }
}
