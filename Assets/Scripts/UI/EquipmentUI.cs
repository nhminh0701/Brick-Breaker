using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] Color[] stateColors;

    UnlockObjPopupUI popupWindow;
    ObjectData equipmentData;
    TextMeshProUGUI equipedObjectText;
    ObjectInventory belongingInventory;
    ObjectInventoryUI objectInventoryUI;
    Image image;

    public void Setup(ObjectData _equipmentData, ObjectInventory _belongingInventory, 
        TextMeshProUGUI _equipText, UnlockObjPopupUI _popupWindow, ObjectInventoryUI _objectInventoryUI)
    {
        equipmentData = _equipmentData;
        popupWindow = _popupWindow;
        equipedObjectText = _equipText;
        belongingInventory = _belongingInventory;
        objectInventoryUI = _objectInventoryUI;
        image = GetComponent<Image>();
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _equipmentData.objectName;
        GetComponent<Button>().onClick.AddListener(OnClickEvent);
    }

    public ObjectData GetObjectData()
    {
        return equipmentData;
    }

    void OnClickEvent()
    {
        if (equipmentData.isUnlocked) OnSelected();
        else OnNotification();
    } 

    public void OnSelected()
    {
        equipedObjectText.text = equipmentData.objectName;
        belongingInventory.SelectObject(equipmentData.objectName);
        objectInventoryUI.ResetUIsStates();
    }

    public void OnUnlock()
    {
        belongingInventory.UnlockObject(equipmentData.objectName);
        equipedObjectText.text = equipmentData.objectName;
        belongingInventory.SelectObject(equipmentData.objectName);
        objectInventoryUI.ResetUIsStates();
    }

    void OnNotification()
    {
        popupWindow.OpenPopup();
        popupWindow.Setup(this);
        // popup a window
        // pass unlock type
        // if money, show button buy (disable that button in the beginning)
    }

    public void RefreshState()
    {
        if (equipmentData.isUnlocked == false)
        {
            image.color = stateColors[2];
        } else
        {
            image.color = equipmentData.isSelecting ? stateColors[0] : stateColors[1];
        }
    }
}
