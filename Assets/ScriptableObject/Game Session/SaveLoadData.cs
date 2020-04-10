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
    public GameData gameData;

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

    public void InitializeData()
    {
        string path = Path.Combine(Application.persistentDataPath, "GameData.json");
        if (!File.Exists(path))
        {
            ResetData();
            SaveDataToPersistent();
        }
        LoadGameDataFromPersistent();
        LoadGameDataToSystem();
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

    /// <summary>
    /// Used to load current progress, recommended initialization phase of the app
    /// </summary>
    void LoadGameDataToSystem()
    {
        gameState = GameState.Instance;

        gameState.inventorySystem.ballInventory.ListObjectData = gameData.listBallData;
        gameState.inventorySystem.paddleInventory.ListObjectData = gameData.listPaddleData;
        gameState.money.SetValue(gameData.money);
        gameState.levelDatabase.NumberOfUnlockedLevel = gameData.maxUnlockedLevel;
        // load max level
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
