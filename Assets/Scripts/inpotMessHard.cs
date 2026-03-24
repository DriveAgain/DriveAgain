using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class inpotMessHard : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panel;          // PopupPanel
    [SerializeField] private TextMeshProUGUI messageText; // PopupText
    [SerializeField] private Button okButton;            // OkButton
    [SerializeField] private Image popupImage;           // PopupImage

    [Header("Sprites")]
    [SerializeField] private Sprite objectSprite;        // תמונת התמרור / חפצים בכביש

    private void Awake()
    {
        okButton.onClick.AddListener(OnOkClicked);

        panel.SetActive(false);

        if (popupImage != null)
            popupImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 0f;      // עוצר את המשחק
        panel.SetActive(true);

        messageText.text = "הימנע מפגיעה בחפצים על הכביש";

        if (popupImage != null && objectSprite != null)
        {
            popupImage.gameObject.SetActive(true);
            popupImage.sprite = objectSprite;
            popupImage.preserveAspect = true;
        }
    }

    private void OnOkClicked()
    {
        Time.timeScale = 1f;      // מחזיר את המשחק
        panel.SetActive(false);
    }
}