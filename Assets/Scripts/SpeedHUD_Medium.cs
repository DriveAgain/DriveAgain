using UnityEngine;
using TMPro;

public class SpeedHUD_Medium : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI warningText;

    [Header("Car")]
    public Rigidbody carRigidbody;
    public float speedLimitKmh = 50f;

    private StarManager starManager;
    private bool wasOverSpeed = false;

    void Start()
    {
        starManager = FindFirstObjectByType<StarManager>();
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (carRigidbody == null) return;

        float speedKmh = carRigidbody.linearVelocity.magnitude * 3.6f;

        if (speedText != null)
            speedText.text = Mathf.Round(speedKmh) + " KM";

        bool isOverSpeed = speedKmh > speedLimitKmh;

        if (warningText != null)
            warningText.gameObject.SetActive(isOverSpeed);

        // אירוע: מעבר ל־OverSpeed (כניסה ל->50)
        if (isOverSpeed && !wasOverSpeed)
        {
            if (starManager != null)
                starManager.LoseStar("Speed > 50");
        }

        wasOverSpeed = isOverSpeed;
    }
}
