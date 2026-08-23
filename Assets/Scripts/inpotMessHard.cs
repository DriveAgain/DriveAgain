using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroPopupHard : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]  TextMeshProUGUI introText;
    [SerializeField]  Image introImage;

    [Header("Sprites")]
    [SerializeField]  Sprite bonusSprite;       // אייקון בונוס זמן
    [SerializeField]  Sprite objectSprite;      // תמרור חפצים בכביש
    
    private int step = 0;

    private void Start()
    {
        Time.timeScale = 0f;
        ShowStep();
    }

    public void OnOkClicked()
    {
        step++;
        ShowStep();
    }

    private void ShowStep()
    {
        introImage.gameObject.SetActive(false);

        if (step == 0)
        {
            // ---------- שלב 1: הימנע מפגיעה בחפצים ----------
            introImage.gameObject.SetActive(true);
            introImage.sprite = objectSprite;
            introImage.preserveAspect = true;

            introText.text =
                "הימנע מפגיעה בחפצים על הכביש\n" +
                "ברמה הקשה יופיעו מכשולים רבים יותר.\n" +
                "פגיעה בחפץ תפחית ניקוד.";
        }
        else if (step == 1)
        {
            // ---------- שלב 2: בונוס זמן ----------
            if (bonusSprite != null)
            {
                introImage.gameObject.SetActive(true);
                introImage.sprite = bonusSprite;
                introImage.preserveAspect = true;
            }

            introText.text =
                "בונוס זמן!\n" +
                "אם תסיים את המסלול בפחות מדקה וחצי\n" +
                "תקבל בונוס של 001 נקודות!";
        }
        else
        {
            // ---------- סיום – התחלת המשחק ----------
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}