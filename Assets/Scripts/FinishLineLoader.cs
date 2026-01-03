using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineLoader : MonoBehaviour
{
    [SerializeField] string nextSceneName = "SuccessScene";
    [SerializeField] string carObjectName = "car";

    [Header("Which level is this?")]
    [SerializeField] int completedLevelIndex = 0; // 0 Tutorial, 1 Easy, 2 Medium, 3 Hard

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != carObjectName)
            return;

        // חשוב: לקבוע איזה שלב הסתיים
        GameState.CompletedLevelIndex = completedLevelIndex;

        StarManager sm = FindFirstObjectByType<StarManager>();
        if (sm != null)
        {
            GameState.Stars = sm.CurrentStars;
            GameState.Reward = GameState.Stars * 100;
        }

        TimerHUD timer = FindFirstObjectByType<TimerHUD>();
        if (timer != null)
            GameState.TotalSeconds = timer.TotalSeconds;

        SceneManager.LoadScene(nextSceneName);
    }
}
