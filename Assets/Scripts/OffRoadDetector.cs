using UnityEngine;

public class OffRoadDetector : MonoBehaviour
{
    private StarManager stars;

    private void Start()
    {
        stars = FindFirstObjectByType<StarManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (stars != null) stars.LoseStar("OffRoad");
    }
}
