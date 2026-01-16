using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouteChoiceUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject routeChoicePanel;
    [SerializeField] private TMP_Text warningText;

    [Header("Buttons")]
    [SerializeField] private Button buttonA;
    [SerializeField] private Button buttonB;
    [SerializeField] private Button buttonC;

    [Header("Config")]
    [SerializeField] private bool freezeGameUntilCorrectChoice = true;
    [SerializeField] private int correctAnswerIndex = 0; // 0=A, 1=B, 2=C
    [SerializeField] private float warningSeconds = 2f;
    [SerializeField] private bool disableButtonsWhileWarning = true;

    [Header("Messages")]
    [SerializeField] private string wrongMessage = "שים/י לב: המסלול חסום. נא לבחור מסלול אחר ולנסות שוב.";
    [SerializeField] private string correctMessage = ""; // אפשר להשאיר ריק

    private Coroutine warningRoutine;

    private void Awake()
    {
        // אם שכחת לחבר כפתורים דרך ה-Inspector — אפשר להוסיף כאן הגנה
        if (warningText != null)
            warningText.gameObject.SetActive(false);
    }

    private void Start()
    {
        // ודאי שהפאנל פתוח בתחילת שלב
        if (routeChoicePanel != null)
            routeChoicePanel.SetActive(true);

        // עצירת המשחק עד בחירה נכונה
        if (freezeGameUntilCorrectChoice)
            Time.timeScale = 0f;

        // חיבור OnClick (לא חובה אם חיברת ידנית ב-Inspector, אבל זה נוח)
        HookButtons();
    }

    private void OnDestroy()
    {
        // כדי לא להשאיר את המשחק תקוע אם עוברים סצנה/מוחקים אובייקט
        if (freezeGameUntilCorrectChoice)
            Time.timeScale = 1f;
    }

    private void HookButtons()
    {
        if (buttonA != null)
        {
            buttonA.onClick.RemoveListener(ChooseA);
            buttonA.onClick.AddListener(ChooseA);
        }

        if (buttonB != null)
        {
            buttonB.onClick.RemoveListener(ChooseB);
            buttonB.onClick.AddListener(ChooseB);
        }

        if (buttonC != null)
        {
            buttonC.onClick.RemoveListener(ChooseC);
            buttonC.onClick.AddListener(ChooseC);
        }
    }

    // --- Public methods for buttons ---
    public void ChooseA() => Choose(0);
    public void ChooseB() => Choose(1);
    public void ChooseC() => Choose(2);

    private void Choose(int chosenIndex)
    {
        if (chosenIndex == correctAnswerIndex)
            CorrectChoice();
        else
            WrongChoice();
    }

    private void CorrectChoice()
    {
        // כיבוי אזהרה אם הייתה
        if (warningRoutine != null)
        {
            StopCoroutine(warningRoutine);
            warningRoutine = null;
        }

        if (warningText != null)
        {
            if (!string.IsNullOrEmpty(correctMessage))
            {
                warningText.text = correctMessage;
                warningText.color = Color.green;
                warningText.gameObject.SetActive(true);
            }
            else
            {
                warningText.gameObject.SetActive(false);
            }
        }

        if (routeChoicePanel != null)
            routeChoicePanel.SetActive(false);

        if (freezeGameUntilCorrectChoice)
            Time.timeScale = 1f;

        // אם בעתיד תרצי “להפעיל” דברים רק אחרי בחירה:
        // פה המקום לקרוא לפונקציה שמפעילה NPC/מכשולים וכו'
        // Example: FindObjectOfType<YourSpawner>()?.StartSpawning();
    }

    private void WrongChoice()
    {
        if (warningText == null) return;

        // עצירה של אזהרה קודמת כדי שלא יתבלגן
        if (warningRoutine != null)
            StopCoroutine(warningRoutine);

        warningRoutine = StartCoroutine(ShowWarningRoutine());
    }

    private IEnumerator ShowWarningRoutine()
    {
        // הצגת הודעה אדומה למטה
        warningText.text = wrongMessage;
        warningText.color = Color.red;
        warningText.gameObject.SetActive(true);

        // אופציונלי: לנעול כפתורים בזמן האזהרה
        if (disableButtonsWhileWarning)
            SetButtonsInteractable(false);

        // חשוב: אם Time.timeScale=0, WaitForSeconds לא ירוץ
        // לכן משתמשים ב-WaitForSecondsRealtime
        yield return new WaitForSecondsRealtime(warningSeconds);

        warningText.gameObject.SetActive(false);

        if (disableButtonsWhileWarning)
            SetButtonsInteractable(true);

        warningRoutine = null;
    }

    private void SetButtonsInteractable(bool value)
    {
        if (buttonA != null) buttonA.interactable = value;
        if (buttonB != null) buttonB.interactable = value;
        if (buttonC != null) buttonC.interactable = value;
    }
}
