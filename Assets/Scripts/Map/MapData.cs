/// <summary>
/// Store Needed data to build a level
/// </summary>
[System.Serializable]
public class MapData 
{
    public float blockSize;
    public BlockData[] listBlockData;

    public MapData(float blockSize, BlockData[] listBlockData)
    {
        this.blockSize = blockSize;
        this.listBlockData = listBlockData;
    }
}

[System.Serializable] 
public class BlockData
{
    public int id;

    public GridPosition position;

    public BlockData(int id, GridPosition position)
    {
        this.id = id;
        this.position = position;
    }

    [System.Serializable]
    public class GridPosition
    {
        public int xPos;
        public int yPos;

        public GridPosition(int xPos, int yPos)
        {
            this.xPos = xPos;
            this.yPos = yPos;
        }
    }
}


