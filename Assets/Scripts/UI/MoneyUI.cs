using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text moneyText;
    IntReference moneyRef;

    void Start()
    {
        moneyRef = GameState.Instance.money;
        SetMoneyUI();
    }

    public void SetMoneyUI()
    {
        moneyText.text = $"$ {moneyRef.Value}";
    }
}
