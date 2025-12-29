using UnityEngine;

public class StopRule : MonoBehaviour
{
    [Header("Stop settings")]
    [SerializeField] private float stopSpeedKmh = 1.0f;     // כמה איטי נחשב "עצירה"
    [SerializeField] private float requiredStopTime = 0.3f; // כמה זמן צריך להיות כמעט 0

    private StarManager stars;
    private Rigidbody playerRb;

    private float stoppedTimer = 0f;
    private bool hasStopped = false;

    private void Start()
    {
        stars = FindFirstObjectByType<StarManager>();
    }

    // נקרא ע"י StopZone כשנכנסים אליו
    public void SetPlayerRigidbody(Rigidbody rb)
    {
        playerRb = rb;
    }

    // נקרא ע"י StopZone בזמן ששוהים עליו
    public void UpdateStopCheck()
    {
        if (playerRb == null || hasStopped) return;

        float speedKmh = playerRb.linearVelocity.magnitude * 3.6f;

        if (speedKmh <= stopSpeedKmh)
        {
            stoppedTimer += Time.deltaTime;
            if (stoppedTimer >= requiredStopTime)
                hasStopped = true;
        }
        else
        {
            stoppedTimer = 0f;
        }
    }

    // כשנכנסים לצומת
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!hasStopped)
        {
            if (stars != null) stars.LoseStar("No Stop");
        }

        // מאפסים לפעם הבאה (אם יהיו עוד צמתים)
        hasStopped = false;
        stoppedTimer = 0f;
    }
}
