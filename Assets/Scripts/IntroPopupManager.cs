using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroPopupManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI introText;
    [SerializeField] Image introImage; // לתמרור
    [SerializeField] Sprite yieldSignSprite; // תמונת תמרור
    [SerializeField] Sprite yellowCarSprite;   // רכב צהוב
     [SerializeField] Sprite bonusSprite;
    int step = 0;

    void Start()
    {
        Time.timeScale = 0f;
        ShowStep();
    }

    public void OnOkClicked()
    {
        step++;
        ShowStep();
    }

    void ShowStep()
    {
        introImage.gameObject.SetActive(false);

        if (step == 0)
        {
            introImage.gameObject.SetActive(true);
            introImage.sprite = yieldSignSprite;

            introText.text =
                "תמרור \"עצור\"\n" +
                "כאשר מופיע תמרור זה,\n" +
                "עליך לעצור עצירה מלאה לפני כניסה לצומת.";
        }
        else if (step == 1)
        {
            introImage.gameObject.SetActive(true);
            introImage.sprite = yellowCarSprite;

            introText.text =
                "זכות קדימה \n" +
                "נא לעצור כאשר רואים רכב צהוב.\n" +
                "כניסה לצומת או כיכר בזמן שהרכב הצהוב עובר\n" +
                "נחשבת טעות.";
        }
           else if (step == 2)
        {
            // ---------- שלב 3: בונוס זמן ----------
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
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
