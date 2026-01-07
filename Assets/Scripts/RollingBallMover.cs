using UnityEngine;

public class RollingBallMover : MonoBehaviour
{
    public Vector3 forceDirection = new Vector3(0, 0, -1);
    public float forcePower = 300f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(forceDirection.normalized * forcePower);
    }
}
