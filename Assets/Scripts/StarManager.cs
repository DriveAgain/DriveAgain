using UnityEngine;
using UnityEngine.SceneManagement;

public class StarManager : MonoBehaviour
{
    [SerializeField] private GameObject[] starIcons; // לגרור Star1, Star2, Star3
    [SerializeField] private string failSceneName = "FailScene";
    [SerializeField] private float loseStarCooldown = 1.0f;

    private int currentStars;
    public int CurrentStars => currentStars;
    private float lastLoseTime = -999f;

    private void Start()
    {
        currentStars = starIcons.Length;
        RefreshUI();
    }

    public void LoseStar(string reason = "")
    {
        if (Time.time - lastLoseTime < loseStarCooldown) return;
        lastLoseTime = Time.time;

        if (currentStars <= 0) return;

        currentStars--;
        Debug.Log($"LoseStar: {reason} | stars={currentStars}");

        RefreshUI();

        if (currentStars <= 0)
            SceneManager.LoadScene(failSceneName);
    }

    private void RefreshUI()
    {
        for (int i = 0; i < starIcons.Length; i++)
            starIcons[i].SetActive(i < currentStars);
    }
}
