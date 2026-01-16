using UnityEngine;

public class BallHitPenalty : MonoBehaviour
{
    [SerializeField] private string carRootTag = "Player";
    [SerializeField] private string reason = "Ball hit";

    private bool alreadyHit = false;
    private StarManager starManager;

    private void Start()
    {
        // יש לך אובייקט בשם GameState עם StarManager עליו, אז זה יימצא.
        starManager = FindObjectOfType<StarManager>();
        if (starManager == null)
            Debug.LogError("BallHitPenalty: StarManager not found in scene!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyHit) return;

        // פגיעה בכל חלק של הרכב (גם ילדים)
        if (!collision.transform.root.CompareTag(carRootTag))
            return;

        alreadyHit = true;

        if (starManager != null)
            starManager.LoseStar(reason);

        Debug.Log("BALL HIT -> LoseStar()");
    }
}
