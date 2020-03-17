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
}