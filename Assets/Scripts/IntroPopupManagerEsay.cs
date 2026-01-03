using UnityEngine;

public class IntroPopupManagerEasy : MonoBehaviour
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
            "המהירות המותרת היא עד 05 קמ\"ש",
            "תזכורת:\n↑ – גז\n↓ – ברקס\n→ – פנייה ימינה\n← – פנייה שמאלה",
            "הכניסה לכיכר היא מצד ימין בלבד"
        });
    }
}
