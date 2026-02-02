using UnityEngine;

public class HelpPanelUI : MonoBehaviour
{
    public GameObject helpPanel;

    private void Start()
    {
        helpPanel.SetActive(false); // בהתחלה סגור
    }

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
}
