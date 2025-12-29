using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineLoader : MonoBehaviour
{
    [SerializeField] string nextSceneName = "SuccessScene";
    [SerializeField] string carObjectName = "car";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != carObjectName)
            return;

        // לשמור כוכבים ל-SuccessScene
        StarManager sm = FindFirstObjectByType<StarManager>();
        if (sm != null)
        {
            GameState.Stars = sm.CurrentStars;
            GameState.Reward = GameState.Stars * 100;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}
