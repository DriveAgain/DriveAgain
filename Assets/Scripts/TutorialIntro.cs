using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialIntro : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;          // PopupPanel
    [SerializeField] private TextMeshProUGUI messageText; // PopupText
    [SerializeField] private Button okButton;            // OkButton
    [SerializeField] private Image popupImage;           // PopupImage (שיצרת)

    [Header("Sprites")]
    [SerializeField] private Sprite speed50Sprite;
   [SerializeField] private Sprite navigationkeys;
   [SerializeField] private Sprite imegeFainle ;

    private int step = 0;

    private void Awake()
    {
        okButton.onClick.AddListener(OnOkClicked);

        panel.SetActive(false);
        if (popupImage != null) popupImage.gameObject.SetActive(false);
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
        HideImage();

        if (step == 0)
        {
            messageText.text = "המהירות המותרת היא עד 05 קמ\"ש";
            ShowImage(speed50Sprite);
        }
        else if (step == 1)
        {
            messageText.text =
                "↑ – גז\n" +
                "↓ – ברקס\n" +
                "→ – פנייה ימינה\n" +
                "← – פנייה שמאלה";
            ShowImage(navigationkeys);
        
        }
        else if (step == 2)
        {
            messageText.text = "סע על הכביש עד נקודת הסיום\n"+
                                 "בהצלחה!!";
            ShowImage(imegeFainle);
        }
        else
        {
            Time.timeScale = 1f;
            panel.SetActive(false);
        }
    }

    private void ShowImage(Sprite sprite)
    {
        if (popupImage == null) return;
        if (sprite == null) return;

        popupImage.gameObject.SetActive(true);
        popupImage.sprite = sprite;
        popupImage.preserveAspect = true;
    }

    private void HideImage()
    {
        if (popupImage != null) popupImage.gameObject.SetActive(false);
    }
}
