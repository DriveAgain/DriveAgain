using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneLoader : MonoBehaviour
{
    public void OpenShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void BackToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
}