using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generate map data for level, based on BlockElement
/// </summary>
public class MapGenerator : MonoBehaviour
{
    [SerializeField] private float blockSize;
    [SerializeField] private int levelId;

    private MapData GenerateMapData()
    {
        List<BlockData> listBlockData = new List<BlockData>();

        BlockElement[] blockElements = FindObjectsOfType<BlockElement>();

        for (int index = 0; index < blockElements.Length; index ++)
        {
            int blockIndex = blockElements[index].blockId;
            Vector2 position = blockElements[index].transform.position;
            BlockData.GridPosition gridPosition = new BlockData.GridPosition(
                Mathf.RoundToInt(position.x / blockSize), Mathf.RoundToInt(position.y / blockSize));

            listBlockData.Add(new BlockData(blockIndex, gridPosition));
        }

        return new MapData(blockSize, listBlockData.ToArray());
    }

    [ContextMenu("Generate Level Data")]
    public void SaveLevelDataToPersistent()
    {
        string path = System.IO.Path.Combine(Application.persistentDataPath, $"{levelId.ToString()}.json");
        string content = JsonUtility.ToJson(GenerateMapData());

        System.IO.File.WriteAllText(path, content);

        Debug.Log($"Level {levelId.ToString()} saved to {path}");
    }
}
