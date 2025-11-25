using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField] GameObject messageUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            messageUI.SetActive(true);
        }
    }
}
