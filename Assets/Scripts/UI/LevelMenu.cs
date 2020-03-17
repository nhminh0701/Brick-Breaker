using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] Button pannelPrefab;
    [SerializeField] Transform buttonSpawnPosition;

    List<Button> listPannels;
    LevelDataBase levelDatabase;

    private void Awake()
    {
        SaveLoadData.Instance.InitializeData();
        levelDatabase = LevelDataBase.Instance;
        SpawnPannels();
    }

    private void SpawnPannels()
    {
        listPannels = new List<Button>();

        for (int index = 0; index < levelDatabase.listLevelDatas.Length; index ++)
        {
            int catchedIndex = index;
            Button newPannel = Instantiate(pannelPrefab, buttonSpawnPosition);
            newPannel.onClick.AddListener(() => AccessLevel(catchedIndex));

            newPannel.GetComponentInChildren<TextMeshProUGUI>().text = (catchedIndex + 1).ToString();
            listPannels.Add(newPannel);
        }
    }

    private void OnEnable()
    {
        RefreshPannelStatus();
    }

    private void RefreshPannelStatus()
    {
        for (int index = 0; index < listPannels.Count; index ++)
        {
            listPannels[index].interactable = index < levelDatabase.NumberOfUnlockedLevel;
        }
    }

    private void AccessLevel(int levelId)
    {
        levelDatabase.currentLevelId = levelId;
        SceneManager.LoadScene(1);
    }
}
