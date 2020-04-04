using UnityEngine;
using System.IO;

/// <summary>
/// Referenced to data containers, load and save data from those references
/// </summary>
[CreateAssetMenu(fileName = "Save Load Data", menuName = "Game Session/ Save Load Data")]
public class SaveLoadData : ScriptableObject
{
    static SaveLoadData instance;
    public static SaveLoadData Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<SaveLoadData>("Game Setting/SaveLoadData");

            return instance;
        }
    }

    [SerializeField] private GameState gameState;
    GameData gameData;

    public GameData GameData
    {
        get
        {
            if (gameData == null)
            {
                InitializeData();
            }

            return gameData;
        }
    }

    public void SaveDataToPersistent()
    {
        ObjectData[] listPaddleData = gameState.inventorySystem.paddleInventory.ListObjectData;
        ObjectData[] listBallData = gameState.inventorySystem.ballInventory.ListObjectData;
        int maxUnlockedLevel = gameState.levelDatabase.NumberOfUnlockedLevel;
        int money = gameState.money.Value;

        GameData gameData = new GameData(maxUnlockedLevel, money, listPaddleData, listBallData);
        string content = JsonUtility.ToJson(gameData);
        string path = Path.Combine(Application.persistentDataPath, "GameData.json");
        File.WriteAllText(path, content);
    }

    /// <summary>
    /// Used to load current progress, recommended initialization phase of the app
    /// </summary>
    public void LoadGameDataToSystem()
    {
        InitializeData();

        gameState.inventorySystem.ballInventory.ListObjectData = GameData.listBallData;
        gameState.inventorySystem.paddleInventory.ListObjectData = GameData.listPaddleData;
        gameState.money.SetValue(GameData.money);
        gameState.levelDatabase.NumberOfUnlockedLevel = GameData.maxUnlockedLevel;
        // load max level
    }

    public void InitializeData()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (!File.Exists(path))
        {
            ResetData();
            SaveDataToPersistent();
        }
        LoadGameDataFromPersistent();
    }

    /// <summary>
    /// Give all dependencies data they need
    /// </summary>
    void LoadGameDataFromPersistent()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData.json");
        string content = File.ReadAllText(path);

        gameData = new GameData();
        JsonUtility.FromJsonOverwrite(content, gameData);
    }

    public void ResetData()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        gameState.ResetGameState();
    }
}
