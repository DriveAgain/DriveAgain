using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI messageText;
    [SerializeField] Button okButton;

    string[] messages;
    int index;

    void Awake()
    {
        okButton.onClick.AddListener(OnOkClicked);
        panel.SetActive(false);
    }

    public void ShowMessages(string[] msgs)
    {
        messages = msgs;
        index = 0;

        Time.timeScale = 0f;       // עוצר את המשחק
        panel.SetActive(true);
        ShowCurrent();
    }

    void ShowCurrent()
    {
        if (messages == null || messages.Length == 0) return;
        messageText.text = messages[index];
    }

    void OnOkClicked()
    {
        index++;

        if (index >= messages.Length)
        {
            panel.SetActive(false);
            Time.timeScale = 1f;   // מחזיר את המשחק
            return;
        }

        ShowCurrent();
    }
}
