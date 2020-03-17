using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game Play Object/Inventory/Inventory System")]
public class InventorySystem : ScriptableObject
{
    public ObjectInventory paddleInventory;
    public ObjectInventory ballInventory;

    public void ResetInventoryData()
    {
        paddleInventory.ResetData();
        ballInventory.ResetData();
    }
}
