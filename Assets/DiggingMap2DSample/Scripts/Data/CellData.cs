using UnityEngine;

/// <summary>
/// Cellの種類
/// </summary>
public enum CellType
{
    Wall,
    Load,
    Start,
}

/// <summary>
/// 各Cellのデータクラス
/// </summary>

public class CellData
{
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="wideIndex">X軸に対するIndex</param>
    /// <param name="heightIndex">Y軸に対するIndex</param>
    /// <param name="mapScale">マップの大きさ</param>
    public CellData(int wideIndex, int heightIndex, int mapScale)
    {
        WideIndex = wideIndex;
        HeightIndex = heightIndex;
        Position = new Vector2Int(wideIndex, heightIndex) * mapScale;
    }

    public CellType CellType { get; set; } = CellType.Wall;
    public Transform Transform { get; set; }

    public int WideIndex { get; private set; }
    public int HeightIndex { get; private set; }
    public Vector2Int Position { get; private set; }
}
