using UnityEngine;

public class StopZone : MonoBehaviour
{
    [SerializeField] private StopRule stopRule;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb != null && stopRule != null)
            stopRule.SetPlayerRigidbody(rb);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (stopRule != null)
            stopRule.UpdateStopCheck();
    }
}
