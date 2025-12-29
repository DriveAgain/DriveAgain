using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroPopupManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI introText;
    [SerializeField] Image introImage; // לתמרור
    [SerializeField] Sprite yieldSignSprite; // תמונת תמרור

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
            introText.text =
                "תזכורת מהחיים האמיתיים\n\n" +
                "צד ימין – גז\n" +
                "צד שמאל – ברקס";
        }
        else if (step == 1)
        {
            introImage.gameObject.SetActive(true);
            introImage.sprite = yieldSignSprite;

            introText.text =
                "תמרור \"תן זכות קדימה\"\n\n" +
                "כאשר מופיע תמרור זה,\n" +
                "עליך לתת זכות קדימה לרכבים החוצים את הצומת.";
        }
        else if (step == 2)
        {
            introText.text =
                "זכות קדימה \n\n" +
                "נא לעצור כאשר רואים רכב צהוב.\n" +
                "כניסה לצומת או כיכר בזמן שהרכב הצהוב עובר\n" +
                "נחשבת טעות.";
        }
        else
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}
