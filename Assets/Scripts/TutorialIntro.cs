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
            "המהירות המותרת היא עד 05 קמש",
            "↑ – גז\n ↓ – ברקס \n → – פנייה ימינה \n ← -פנייה שמאלה"
        });
    }
}
