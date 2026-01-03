using UnityEngine;
using TMPro;

public class SuccessUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI starsText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI timeText;

    void Start()
    {
        if (starsText != null)
            starsText.text = "כוכבים: " + GameState.Stars + "/3";

        if (rewardText != null)
            rewardText.text = "" + GameState.Reward;

        int m = GameState.TotalSeconds / 60;
        int s = GameState.TotalSeconds % 60;

        if (timeText != null)
            timeText.text = $"זמן: {m:00}:{s:00}";
    }
}
