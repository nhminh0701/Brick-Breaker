using UnityEngine;
using System.Linq;

/// <summary>
/// Central data references to the system and to save current progress
/// </summary>
[CreateAssetMenu(fileName = "GameState", menuName = "Game Session/Game State")]
public class GameState : ScriptableObject
{
    private static GameState instance;

    public static GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.LoadAll<GameState>("Game Setting").FirstOrDefault();
                SaveLoadData.Instance.LoadGameDataToSystem();
            }

            return instance;
        }
    }

    public InventorySystem inventorySystem;
    public IntReference money;
    public LevelDataBase levelDatabase;

    public SaveLoadData saveLoadData;

    public void ResetGameState()
    {
        inventorySystem.ResetInventoryData();
        money.SetValue(0);
        levelDatabase.ResetLevelDataBase();
    }
}
