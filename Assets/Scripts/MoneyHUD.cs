using UnityEngine;
using TMPro;

public class MoneyHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public static int Money = 0; 

    void Update()
    {
        if (moneyText != null)
            moneyText.text = "$" + Money;
    }
}
