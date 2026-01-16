using UnityEngine;

public class NpcCarTrigger : MonoBehaviour
{
    [SerializeField] private NpcCarCrossing npcCar;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (!other.transform.root.CompareTag("Player"))
            return;

        activated = true;
        npcCar.StartCrossing();
    }
}
