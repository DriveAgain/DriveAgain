using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedHUD : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI warningText;
    public Image[] stars;   // Star1, Star2, Star3

    [Header("Car")]
    public Rigidbody carRigidbody;
    public float speedLimitKmh = 50f;

    int currentStars;
    bool wasOverSpeed = false;

    void Start()
    {
        currentStars = stars.Length;
        GameState.Stars = currentStars;
        GameState.Reward = currentStars * 100;
    }

    void Update()
    {
        if (carRigidbody == null) return;

        float speedKmh = carRigidbody.linearVelocity.magnitude * 3.6f;
        speedText.text = Mathf.Round(speedKmh) + " KM";

        bool isOverSpeed = speedKmh > speedLimitKmh;
        warningText.gameObject.SetActive(isOverSpeed);

        // זיהוי "אירוע חריגה" (כניסה ל־>50)
        if (isOverSpeed && !wasOverSpeed)
        {
            LoseStar();
        }

        wasOverSpeed = isOverSpeed;
    }

    void LoseStar()
    {
        if (currentStars <= 0) return;

        currentStars--;
        stars[currentStars].gameObject.SetActive(false);
        GameState.Stars = currentStars;
        GameState.Reward = currentStars * 100;
    }
}
