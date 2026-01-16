using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class OpeningLoginManager : MonoBehaviour
{
    [Header("Login (Main Screen)")]
    [SerializeField] private TMP_InputField loginUsernameInput;
    [SerializeField] private TextMeshProUGUI loginStatusText; // במסך הראשי - רק הודעות של כניסה

    [Header("Not Registered Panel")]
    [SerializeField] private GameObject notRegisteredPanel;   // חלונית: "השחקן לא רשום, רוצה להירשם?"

    [Header("Register Panel")]
    [SerializeField] private GameObject registerPanel;        // חלונית הרשמה
    [SerializeField] private TMP_InputField registerUsernameInput;
    [SerializeField] private TextMeshProUGUI registerStatusText; // בתוך חלונית הרשמה בלבד

    [Header("Settings")]
    [SerializeField] private string nextSceneName = "EasyScenes";
    [SerializeField] private string fixedPassword = "DriveAgain2026!";

    private bool isBusy = false;

    async void Start()
    {
        await UnityServices.InitializeAsync();

        if (notRegisteredPanel != null) notRegisteredPanel.SetActive(false);
        if (registerPanel != null) registerPanel.SetActive(false);

        ClearLoginStatus();
        ClearRegisterStatus();
    }

    // =========================
    // MAIN BUTTONS
    // =========================

    // כפתור "כניסה"
    public async void OnLoginClicked()
    {
        if (isBusy) return;

        string username = GetLoginUsername();
        if (string.IsNullOrEmpty(username))
        {
            ShowLoginError("נא למלא את השם");
            return;
        }

        if (!IsUsernameValid(username, out string whyNot))
        {
            ShowLoginError(whyNot);
            return;
        }

        isBusy = true;
        ShowLoginStatus("מתחבר...");

        bool ok = await TrySignIn(username);

        isBusy = false;

        // תמיד מאפסים את שדה הכניסה אחרי ניסיון (כמו שביקשת)
        ClearLoginInput();

        if (ok)
        {
            ClearLoginStatus();
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // לא רושמים הודעה במסך הראשי! רק קופצת החלונית.
            ClearLoginStatus();
            ShowNotRegisteredPanel();
        }
    }

    // כפתור "להירשם" במסך הראשי
    public void OnOpenRegisterClicked()
    {
        if (isBusy) return;

        // פותחים חלון הרשמה, ומעתיקים את הטקסט מהמסך הראשי אם יש
        string fromLogin = GetLoginUsername();
        ShowRegisterPanel(prefill: fromLogin);

        // לא מציגים סטטוס במסך הראשי
        ClearLoginStatus();
    }

    // =========================
    // NOT REGISTERED PANEL
    // =========================

    // בתוך חלונית "לא רשום" -> כפתור "להירשם"
    public void OnNotRegisteredRegisterClicked()
    {
        if (isBusy) return;

        HideNotRegisteredPanel();

        // נעדיף לקחת שם שהמשתמש הקליד במסך הראשי (אם נשאר),
        // אבל אנחנו מאפסים אחרי Login, אז בדרך כלל יהיה ריק.
        // לכן נשאיר חלון הרשמה פתוח עם שדה ריק/מה שיש.
        ShowRegisterPanel(prefill: "");
    }

    public void OnNotRegisteredCloseClicked()
    {
        HideNotRegisteredPanel();
    }

    // =========================
    // REGISTER PANEL
    // =========================

    // כפתור "הירשם" בחלונית הרשמה
    public async void OnRegisterSubmitClicked()
    {
        if (isBusy) return;

        string username = GetRegisterUsername();
        if (string.IsNullOrEmpty(username))
        {
            ShowRegisterError("נא להכניס שם משתמש");
            return;
        }

        if (!IsUsernameValid(username, out string whyNot))
        {
            ShowRegisterError(whyNot);
            return;
        }

        isBusy = true;
        ShowRegisterStatus("נרשם...");

        bool ok = await TrySignUp(username);

        isBusy = false;

        if (ok)
        {
            // הצלחה: סוגרים חלון, מאפסים שדות, ומראים במסך הראשי הודעה נחמדה
            HideRegisterPanel();
            ClearRegisterInput();
            ClearRegisterStatus();

            ShowLoginStatus("נרשמת בהצלחה! עכשיו אפשר להיכנס 😊");

            // לפי הבקשה שלך: לא לשמור טקסט בשדות
            ClearLoginInput();
        }
        else
        {
            // אם נכשל - לא תמיד "שם תפוס": יכול להיות גם פורמט לא תקין/בעיה אחרת.
            // אבל רוב הזמן זה "כבר קיים", אז נציג הודעה כללית + נרמז ששם תפוס.
            ShowRegisterError("לא הצלחתי להירשם, יכול להיות שהשם תפוס או לא תקין. נסה שם אחר");
        }
    }

    // X בחלונית הרשמה
    public void OnRegisterCloseClicked()
    {
        HideRegisterPanel();
        ClearRegisterInput();
        ClearRegisterStatus();
    }

    // =========================
    // AUTH HELPERS
    // =========================

    private async Task<bool> TrySignIn(string username)
    {
        try
        {
            // כדי שלא יהיה מצב "תקוע" על משתמש אחר
            if (AuthenticationService.Instance.IsSignedIn)
                AuthenticationService.Instance.SignOut();

            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, fixedPassword);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private async Task<bool> TrySignUp(string username)
    {
        try
        {
            if (AuthenticationService.Instance.IsSignedIn)
                AuthenticationService.Instance.SignOut();

            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, fixedPassword);
            return true;
        }
        catch
        {
            return false;
        }
    }

    // =========================
    // UI HELPERS
    // =========================

    private void ShowNotRegisteredPanel()
    {
        if (notRegisteredPanel != null)
            notRegisteredPanel.SetActive(true);
    }

    private void HideNotRegisteredPanel()
    {
        if (notRegisteredPanel != null)
            notRegisteredPanel.SetActive(false);
    }

    private void ShowRegisterPanel(string prefill)
    {
        if (registerPanel != null)
            registerPanel.SetActive(true);

        ClearRegisterStatus();


        // למלא אוטומטית אם יש שם מהמסך הראשי
        if (registerUsernameInput != null)
            registerUsernameInput.text = string.IsNullOrEmpty(prefill) ? "" : prefill.Trim();
    }

    private void HideRegisterPanel()
    {
        if (registerPanel != null)
            registerPanel.SetActive(false);
    }

    private string GetLoginUsername()
    {
        return loginUsernameInput ? loginUsernameInput.text.Trim() : "";
    }

    private string GetRegisterUsername()
    {
        return registerUsernameInput ? registerUsernameInput.text.Trim() : "";
    }

    private void ClearLoginInput()
    {
        if (loginUsernameInput != null)
            loginUsernameInput.text = "";
    }

    private void ClearRegisterInput()
    {
        if (registerUsernameInput != null)
            registerUsernameInput.text = "";
    }

    private void ShowLoginStatus(string msg)
    {
        if (loginStatusText == null) return;
        loginStatusText.text = msg;
        loginStatusText.color = Color.black;
    }

    private void ShowLoginError(string msg)
    {
        if (loginStatusText == null) return;
        loginStatusText.text = msg;
        loginStatusText.color = Color.red;
    }

    private void ClearLoginStatus()
    {
        if (loginStatusText == null) return;
        loginStatusText.text = "";
        loginStatusText.color = Color.black;
    }

    private void ShowRegisterStatus(string msg)
    {
        if (registerStatusText == null) return;
        registerStatusText.text = msg;
        registerStatusText.color = Color.black;
    }

    private void ShowRegisterError(string msg)
    {
        if (registerStatusText == null) return;
        registerStatusText.text = msg;
        registerStatusText.color = Color.red;
    }

    private void ClearRegisterStatus()
    {
        if (registerStatusText == null) return;
        registerStatusText.text = "";
        registerStatusText.color = Color.black;
    }

    // =========================
    // VALIDATION
    // =========================

    // Unity Username requirements בדרך כלל:
    // מינימום 3, מקסימום 20, ורק אותיות/מספרים ותווים מסוימים (כולל . _ - @)
    private bool IsUsernameValid(string username, out string error)
    {
        error = "";

        if (username.Length < 3 || username.Length > 20)
        {
            error = "שם משתמש חייב להיות בין 3 ל-02 תווים";
            return false;
        }

        for (int i = 0; i < username.Length; i++)
        {
            char c = username[i];
            bool ok =
                (c >= 'a' && c <= 'z') ||
                (c >= 'A' && c <= 'Z') ||
                (c >= '0' && c <= '9') ||
                c == '.' || c == '_' || c == '-' || c == '@';

            if (!ok)
            {
                error = "מותר רק אותיות באנגלית , מספרים או תווים";
                return false;
            }
        }

        return true;
    }
}
