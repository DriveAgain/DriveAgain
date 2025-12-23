using UnityEngine;

public class TutorialIntro : MonoBehaviour
{
    [SerializeField] PopupManager popup;  // אופציונלי - לא חייבים לגרור

    void Awake()
    {
        // אם לא גררנו ידנית - תופס אוטומטית מהאובייקט הזה (UIManager)
        if (popup == null) popup = GetComponent<PopupManager>();
    }

    void Start()
    {
        // 2 הודעות אחת אחרי השנייה עם OK
        popup.ShowMessages(new string[]
        {
            "Real life reminder:\nRight side = gas\nLeft side = brakes",
            "The speed limit is up to 50 km/h."
        });
    }
}
