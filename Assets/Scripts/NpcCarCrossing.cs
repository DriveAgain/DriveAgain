using UnityEngine;

public class NpcCarCrossing : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float speed = 6f;

    private bool canMove = false;

    public void StartCrossing()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove || targetPoint == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint.position,
            speed * Time.deltaTime
        );
    }
}
