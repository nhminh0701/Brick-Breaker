using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EquipmentUI : MonoBehaviour
{
    [SerializeField] Color[] stateColors;

    #region Listeners
    GameObject popupWindow;
    ObjectData equipmentData;
    TextMeshProUGUI equipedObjectText;
    ObjectInventory belongingInventory;
    List<EquipmentUI> equipmentUIs;
    #endregion

    Image image;

    /// <summary>
    /// Dependencies Injection
    /// </summary>
    /// <param name="_equipmentData"></param>
    /// <param name="_belongingInventory"></param>
    /// <param name="_equipText"></param>
    /// <param name="_popupWindow"></param>
    /// <param name="_equipmentUIs"></param>
    public void Setup(ObjectData _equipmentData, ObjectInventory _belongingInventory, 
        TextMeshProUGUI _equipText, GameObject _popupWindow, List<EquipmentUI> _equipmentUIs = null)
    {
        equipmentData = _equipmentData;
        popupWindow = _popupWindow;
        equipedObjectText = _equipText;
        belongingInventory = _belongingInventory;
        equipmentUIs = _equipmentUIs;

        image = GetComponent<Image>();

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _equipmentData.objectName;
        GetComponent<Button>().onClick.AddListener(OnClickEvent);

        RefreshState();
    }

    void OnClickEvent()
    {
        if (equipmentData.isUnlocked) OnSelected();
        else OnNotification();
    } 

    void OnSelected()
    {
        equipedObjectText.text = equipmentData.objectName;
        belongingInventory.SelectObject(equipmentData.objectName);

        for (int index = 0; index < equipmentUIs.Count; index ++)
        {
            int catchedIndex = index;
            equipmentUIs[catchedIndex].RefreshState();
        }
    }

    void OnNotification()
    {
        // popup a window
        // pass unlock type
        // if money, show button buy (disable that button in the beginning)
    }

    void RefreshState()
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
