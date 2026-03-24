using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroPopupEasy : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button okButton;
    [SerializeField] private Image introImage; // ה-Image ששמת בפאנל

    [Header("Sprites (Easy)")]
    [SerializeField] private Sprite speed50Sprite;   // תמרור 50
    [SerializeField] private Sprite enterFromRightSprite; // כניסה מצד ימין
    [SerializeField] private Sprite navigationkeys;

    private int step = 0;

    private void Awake()
    {
        okButton.onClick.AddListener(OnOkClicked);
        panel.SetActive(false);
        if (introImage != null) introImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
        ShowStep();
    }

    private void OnOkClicked()
    {
        step++;
        ShowStep();
    }

    private void ShowStep()
    {

        if (step == 0)
        {
            messageText.text = "המהירות המותרת היא עד 05 קמ\"ש";
            ShowImage(speed50Sprite);
        }
        else if (step == 1)
        {
            messageText.text =
                "תזכורת:\n" +
                "↑ – גז\n" +
                "↓ – ברקס\n" +
                "→ – פנייה ימינה\n" +
                "← – פנייה שמאלה";
            ShowImage(navigationkeys);
        }
        else if (step == 2)
        {
            messageText.text = "הכניסה לכיכר היא מצד ימין בלבד";
             ShowImage(enterFromRightSprite);
        }
        else
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
        }
    }

    private void ShowImage(Sprite sprite)
    {
        if (introImage == null) return;
        if (sprite == null) return;

        introImage.gameObject.SetActive(true);
        introImage.sprite = sprite;
        introImage.preserveAspect = true;
    }
}
