using UnityEngine;
using UnityEngine.SceneManagement;

public class FailManager : MonoBehaviour
{
    [SerializeField] private string failSceneName = "FailScene";
    private bool failed = false;

    public void Fail(string reason)
    {
        if (failed) return;
        failed = true;
        Debug.Log("FAIL: " + reason);
        SceneManager.LoadScene(failSceneName);
    }
}
