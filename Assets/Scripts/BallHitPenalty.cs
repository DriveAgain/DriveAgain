using UnityEngine;

public class BallHitPenalty : MonoBehaviour
{
    [SerializeField] string carTag = "Player";
    [SerializeField] int starsToLose = 1;

    private bool alreadyHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyHit) return;

        // מחפש Tag על האובייקט שפגע או על אחד ההורים שלו
        Transform t = collision.transform;
        bool isCar = t.CompareTag(carTag) || t.GetComponentInParent<Transform>().CompareTag(carTag);

        // אם זה לא על אותו אובייקט, נבדוק "בצורה בטוחה" את כל ההורים:
        if (!isCar)
        {
            var car = collision.collider.GetComponentInParent<CarMarker>();
            if (car == null) return;
        }

        alreadyHit = true;

        GameState.Stars = Mathf.Max(0, GameState.Stars - starsToLose);
        Debug.Log("Hit ball! Stars now: " + GameState.Stars);
    }
}
