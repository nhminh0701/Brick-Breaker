using System;

/// <summary>
/// Storing data of current progress of user
/// </summary>
[Serializable]
public class GameData
{
    public int maxUnlockedLevel = 0;
    public int money = 0;

    public ObjectData[] listPaddleData;
    public ObjectData[] listBallData;

    public GameData(int maxUnlockedLevel = 0, int money = 0, ObjectData[] listPaddleData = default(ObjectData[]),
        ObjectData[] listBallData = default(ObjectData[]))
    {
        this.maxUnlockedLevel = maxUnlockedLevel;
        this.money = money;
        this.listPaddleData = listPaddleData;
        this.listBallData = listBallData;
    }

    public static GameData GetDataFromState(GameState gameState)
    {
        ObjectData[] listPaddleData = gameState.inventorySystem.paddleInventory.ListObjectData;
        ObjectData[] listBallData = gameState.inventorySystem.ballInventory.ListObjectData;
        int maxUnlockedLevel = gameState.levelDatabase.NumberOfUnlockedLevel;
        int money = gameState.money.Value;
        GameData gameData = new GameData(maxUnlockedLevel, money, listPaddleData, listBallData);
        return gameData;
    }
}