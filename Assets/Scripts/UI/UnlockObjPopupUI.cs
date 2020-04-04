using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockObjPopupUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI purchaseParText;
    [SerializeField] Button buyButton;
    [SerializeField] IntReference moneyRef;
    [SerializeField] MoneyUI moneyUI;
    ObjectData objectData;
    EquipmentUI equipmentUI;

    int price;
    bool canBuy;

    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }

    public void Setup(EquipmentUI _equipmentUI)
    {
        equipmentUI = _equipmentUI;
        objectData = _equipmentUI.GetObjectData();
        SetUIState();
    }

    void SetUIState()
    {
        SetMoneyText();
        SetButtonState();
    }

    void SetMoneyText()
    {
        price = objectData.purchaseData.purchasePar;
        purchaseParText.text = price.ToString();
        canBuy = price <= moneyRef.Value;
        purchaseParText.color = canBuy ? Color.black : Color.red;
    }

    void SetButtonState()
    {
        buyButton.interactable = canBuy;
        //if (!canBuy) return;
        //buyButton.onClick.AddListener(BuyEquipment);
    }

    public void BuyEquipment()
    {
        if (objectData == null) return;
        moneyRef.Value -= price;
        moneyUI.SetMoneyUI();
        equipmentUI.OnUnlock();
        ClosePopUp();
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }
}
