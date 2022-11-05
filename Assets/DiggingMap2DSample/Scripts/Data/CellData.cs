using UnityEngine;

public enum CellType
{
    Wall,
    Load,
    Start,
}

public class CellData
{
    public CellData(int wideIndex, int heightIndex, int mapScale)
    {
        WideIndex = wideIndex;
        HeightIndex = heightIndex;
        Position = new Vector2Int(wideIndex, heightIndex) * mapScale;
    }

    public CellType CellType { get; set; } = CellType.Wall;

    public int WideIndex { get; private set; }
    public int HeightIndex { get; private set; }
    public Vector2Int Position { get; private set; }
}
