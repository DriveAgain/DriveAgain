using UnityEngine;
using TMPro;

public class SuccessUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI starsText;
    [SerializeField] TextMeshProUGUI rewardText;

    void Start()
    {
        starsText.text = "Stars: " + GameState.Stars + "/3";
        rewardText.text = "Reward: " + GameState.Reward;
    }
}
