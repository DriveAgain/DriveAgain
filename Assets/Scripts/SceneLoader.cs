using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // לא נוגעים: נשאר לכל המקומות שכבר משתמשים בזה
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    // חדש: רק לכפתור "כניסה כאורח"
    public void GuestLoadLevelSelect()
    {
        GuestSession.StartGuest();
        SceneManager.LoadScene("LevelSelectScene");
    }
}
