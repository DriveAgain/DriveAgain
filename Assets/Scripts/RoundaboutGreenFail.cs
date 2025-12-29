using UnityEngine;

public class RoundaboutGreenFail : MonoBehaviour
{
    private FailManager fail;

    private void Start()
    {
        fail = FindFirstObjectByType<FailManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // רק אם זה הקדימה של הרכב
        if (!other.CompareTag("PlayerFront")) return;

        if (fail != null)
            fail.Fail("Stuck in roundabout (hit green)");
    }
}
