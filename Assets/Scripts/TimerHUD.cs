using UnityEngine;
using TMPro;

public class TimerHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float t;

    void Update()
    {
        t += Time.deltaTime;

        int total = Mathf.FloorToInt(t);
        int minutes = total / 60;
        int seconds = total % 60;

        if (timerText != null)
            timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
