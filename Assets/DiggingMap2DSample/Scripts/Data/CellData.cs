using UnityEngine;

/// <summary>
/// Cell�̎��
/// </summary>
public enum CellType
{
    Wall,
    Load,
    Start,
}

/// <summary>
/// �eCell�̃f�[�^�N���X
/// </summary>

public class CellData
{
    /// <summary>
    /// ������
    /// </summary>
    /// <param name="wideIndex">X���ɑ΂���Index</param>
    /// <param name="heightIndex">Y���ɑ΂���Index</param>
    /// <param name="mapScale">�}�b�v�̑傫��</param>
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
