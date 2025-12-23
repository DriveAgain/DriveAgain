using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadTutorial : MonoBehaviour
{
    public void Load()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
