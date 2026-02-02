using System.Collections;
using UnityEngine;
using TMPro;

public class PenaltyTrigger : MonoBehaviour
{
    [Header("Car")]
    [SerializeField] private string carTag = "Player";

    [Header("Stars")]
    [SerializeField] private StarManager starManager;   // <-- לגרור מהסצנה

    [Header("UI Warning")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private string message = "הכניסה לכיכר היא מצד ימין בלבד!!";
    [SerializeField] private float showSeconds = 3f;

    private bool alreadyPenalized = false;

    private void Start()
    {
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(carTag)) return;
        if (alreadyPenalized) return;
        alreadyPenalized = true;

        // מוריד כוכב דרך המנהל שלך
        if (starManager != null)
            starManager.LoseStar("Left road penalty");
        else
            Debug.LogError("PenaltyTrigger: starManager is not assigned!");

        // הודעה ל-3 שניות
        if (warningText != null)
        {
            StopAllCoroutines();
            StartCoroutine(ShowWarning());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(carTag)) return;
        alreadyPenalized = false;
    }

    private IEnumerator ShowWarning()
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(showSeconds);
        warningText.gameObject.SetActive(false);
    }
}
