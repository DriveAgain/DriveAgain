using UnityEngine;

public class BallTriggerActivator : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;
    [SerializeField] Vector3 forceDirection = new Vector3(1, 0, 0);
    [SerializeField] float forcePower = 300f;
    [SerializeField] string carTag = "Player";

    bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;
        if (!other.CompareTag(carTag)) return;

        activated = true;

        // להפעיל את הכדור
        ballRb.isKinematic = false;
        ballRb.AddForce(forceDirection.normalized * forcePower);

        // אופציונלי: למחוק את הטריגר אחרי הפעלה
        Destroy(gameObject);
    }
}
