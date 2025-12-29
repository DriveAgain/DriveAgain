using UnityEngine;
using TMPro;
using System.Collections;

public class PopupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private float showSeconds = 2f;

    Coroutine current;

    public void Show(string msg)
    {
        if (popupText == null) return;

        if (current != null) StopCoroutine(current);
        current = StartCoroutine(ShowRoutine(msg));
    }

    IEnumerator ShowRoutine(string msg)
    {
        popupText.text = msg;
        popupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showSeconds);
        popupText.gameObject.SetActive(false);
    }
}
