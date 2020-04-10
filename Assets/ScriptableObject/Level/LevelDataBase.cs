using UnityEngine;
using System.Linq;

/// <summary>
/// Storing Level Data and status, in charge of updating data upon clear level
/// </summary>
[CreateAssetMenu(fileName = "LevelDataBase", menuName = "Level/DataBase")]
public class LevelDataBase : ScriptableObject
{
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
        set {
            numberOfUnlockedLevel = value;
        }
    }

    public void UnlockNewLevel()
    {
        GetRewardData();
        UpdateNewLevelId();
    }

    void GetRewardData()
    {
        GameState currentState= GameState.Instance;
        LevelReward rewardCurrentLv = CurrentLevelData.reward;
        int newMoneyVal = currentState.money.Value + rewardCurrentLv.gold;
        currentState.money.SetValue(newMoneyVal);
    }

    private void UpdateNewLevelId()
    {
        if (currentLevelId == listLevelDatas.Length - 1) return;
        currentLevelId++;
        SaveLoadData.Instance.SaveDataToPersistent();
    }

    public void ResetLevelDataBase()
    {
        currentLevelId = 0;
        numberOfUnlockedLevel = 0;
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
