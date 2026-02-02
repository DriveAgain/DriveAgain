using System.Collections;
using UnityEngine;
using TMPro;

public class StopRule : MonoBehaviour
{
    [Header("Stop settings")]
    [SerializeField] private float stopSpeedKmh = 1.0f;
    [SerializeField] private float requiredStopTime = 0.3f;

    [Header("Warning UI")]
    [SerializeField] private GameObject warningUI; // <-- לגרור את האובייקט של הטקסט
    [SerializeField] private float warningSeconds = 2f;

    private StarManager stars;
    private Rigidbody playerRb;

    private float stoppedTimer = 0f;
    private bool hasStopped = false;

    private void Start()
    {
        stars = FindFirstObjectByType<StarManager>();

        if (warningUI != null)
            warningUI.SetActive(false);
    }

    public void SetPlayerRigidbody(Rigidbody rb)
    {
        playerRb = rb;
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!hasStopped)
        {
            if (stars != null) stars.LoseStar("No Stop");
            ShowWarning();
        }

        hasStopped = false;
        stoppedTimer = 0f;
    }

    private void ShowWarning()
    {
        if (warningUI == null) return;
        StopAllCoroutines();
        StartCoroutine(WarningRoutine());
    }

    private IEnumerator WarningRoutine()
    {
        warningUI.SetActive(true);
        yield return new WaitForSeconds(warningSeconds);
        warningUI.SetActive(false);
    }
}
