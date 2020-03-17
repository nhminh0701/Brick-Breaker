using UnityEngine;
using System.Linq;

/// <summary>
/// Storing Level Data and status, in charge of updating data upon clear level
/// </summary>
[CreateAssetMenu(fileName = "LevelDataBase", menuName = "Level/DataBase")]
public class LevelDataBase : ScriptableObject
{
    private static LevelDataBase instance;
    public static LevelDataBase Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.LoadAll<LevelDataBase>("Game Setting").FirstOrDefault();
            }

            return instance;
        }
    }

    private void OnEnable()
    {
        instance = Resources.LoadAll<LevelDataBase>("Game Setting").FirstOrDefault();
    }

    public LevelData[] listLevelDatas;
    /// <summary>
    /// Get Data corresponding current level id
    /// </summary>
    public LevelData CurrentLevelData
    {
        get
        {
            return listLevelDatas[currentLevelId];
        }
    }

    /// <summary>
    /// Level being selected by user
    /// </summary>
    public int currentLevelId;

    private int numberOfUnlockedLevel;
    /// <summary>
    /// Maximum accessable levels
    /// </summary>
    public int NumberOfUnlockedLevel {
        get
        {
            if (currentLevelId > numberOfUnlockedLevel - 1)
                numberOfUnlockedLevel = currentLevelId + 1;
            return numberOfUnlockedLevel;
        }
        // For data loading purpose
        set { }
    }

    public void UnlockNewLevel()
    {
        if (currentLevelId == listLevelDatas.Length - 1) return;
        currentLevelId++;
    }

    public void ResetLevelDataBase()
    {
        currentLevelId = 0;
    }
}

[System.Serializable]
public class LevelData
{
    public LevelReward reward;
}

[System.Serializable]
public class LevelReward
{
    public int gold;
    public RewardObj rewardObj;
}

[System.Serializable]
public class RewardObj
{
    public string objectName;
    public ObjectType type;
}
