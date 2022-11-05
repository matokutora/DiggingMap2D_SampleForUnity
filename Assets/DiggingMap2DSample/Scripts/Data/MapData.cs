using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マップを生成するためのデータクラス
/// </summary>

[System.Serializable]
public class MapData
{
    /// <summary>
    /// 各CellTypeに対するPrefabのデータ
    /// </summary>
    [System.Serializable]
    public class TipData
    {
        [SerializeField] CellType _cellType;
        [SerializeField] GameObject _tipPrefab;

        public CellType CellType => _cellType;
        public GameObject TipPrefab => _tipPrefab;
    }

    [SerializeField] int _wideSize;
    [SerializeField] int _heightSize;
    [SerializeField] int _mapScale;
    [SerializeField] List<TipData> _tipDataList;

    // Collect. 要素数が奇数出なければ奇数に変換
    public int WideSize => _wideSize % 2 != 0 ? _wideSize : _wideSize + 1;
    public int HeightSize => _heightSize % 2 != 0 ? _heightSize : _heightSize + 1;

    public int MapScale => Mathf.Abs(_mapScale);

    /// <summary>
    /// 特定のマップチップの取得
    /// </summary>
    /// <param name="cellType"></param>
    /// <returns>GameObjectPrefab</returns>
    public GameObject GetTipData(CellType cellType)
    {
        return _tipDataList.Find(t => t.CellType == cellType).TipPrefab;
    }
}
