using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectInventoryUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] UnlockObjPopupUI popupWindow;
    [SerializeField] EquipmentUI pannelPrefab;

    [Header("Ball Region")]
    [SerializeField] Transform ballUISpawnPosition;
    [SerializeField] TextMeshProUGUI equipedBallText;

    [Header("Paddle Region")]
    [SerializeField] Transform paddleUISpawnPosition;
    [SerializeField] TextMeshProUGUI equipedPaddleText;

    ObjectInventory ballInventory;
    List<EquipmentUI> equipmentUIs;
    InventorySystem inventorySystem;

    private void Awake()
    {
        SaveLoadData.Instance.InitializeData();
        SetupUI();
    }

    void SetupUI()
    {
        inventorySystem = GameState.Instance.inventorySystem;

        SpawnInventoryUI(equipedBallText, inventorySystem.ballInventory, ballUISpawnPosition);
        SpawnInventoryUI(equipedPaddleText, inventorySystem.paddleInventory, paddleUISpawnPosition);
    }

    void SpawnInventoryUI(TextMeshProUGUI _equipedObjectText, ObjectInventory _objectInventory, Transform _spawningPosition)
    {
        _equipedObjectText.text = _objectInventory.SelectingObject.objectData.objectName;

        List<GamePlayObjFactory> listObjectFactory = _objectInventory.factories;
        equipmentUIs = new List<EquipmentUI>();

        for (int index = 0; index < listObjectFactory.Count; index ++)
        {
            EquipmentUI equipmentUI = Instantiate(pannelPrefab, _spawningPosition);
            equipmentUI.Setup(listObjectFactory[index].objectData, _objectInventory, 
                _equipedObjectText, popupWindow, this);
            equipmentUIs.Add(equipmentUI);
        }
        ResetUIsStates();
    }

    public void ResetUIsStates()
    {
        for (int index = 0; index < equipmentUIs.Count; index ++)
        {
            equipmentUIs[index].RefreshState();
        }
    }
}
