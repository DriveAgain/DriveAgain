using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineLoader : MonoBehaviour
{
    [SerializeField] string nextSceneName = "SuccessScene";
    [SerializeField] string carObjectName = "car";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != carObjectName)
            return;

 
        // --- מעבר סצנה ---
        SceneManager.LoadScene(nextSceneName);
    }
}
