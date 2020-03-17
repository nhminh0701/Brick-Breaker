using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public void ConstructLevel(int levelId)
    {
        string serializedMapData = Resources.Load<TextAsset>($"Level Data/{levelId.ToString()}").text;

        MapData mapData = JsonUtility.FromJson<MapData>(serializedMapData);

        float blockSize = mapData.blockSize;

        for (int index = 0; index < mapData.listBlockData.Length; index++)
        {
            BlockData blockData = mapData.listBlockData[index];
            Vector2 spawnPosition = new Vector2(blockData.position.xPos * blockSize,
                blockData.position.yPos * blockSize);

            GameObject blockPrefab = Resources.Load<GameObject>($"Block Prefab/{blockData.id.ToString()}");
            Instantiate(blockPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
