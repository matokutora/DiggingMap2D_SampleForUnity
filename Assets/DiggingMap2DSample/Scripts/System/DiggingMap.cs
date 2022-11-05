using System.Collections.Generic;
using UnityEngine;

public class DiggingMap
{
    int _errorCount = 0;

    Vector2Int[] _digDir = new Vector2Int[4]
    {
        new Vector2Int( -1, 0),
        new Vector2Int( 1, 0),
        new Vector2Int( 0, -1),
        new Vector2Int( 0, 1)
    };

    CellData[,] _cellDatas;
    List<Vector2Int> _callbackList = new List<Vector2Int>();

    readonly int DigOverflowCount = 50;

    /// <summary>
    /// 穴掘り法の開始
    /// </summary>
    /// <param name="data"></param>
    public void Create(MapData data)
    {
        _cellDatas = new CellData[data.WideSize, data.HeightSize];

        Initalize(data);

        Vector2Int digAddress = StartDig(data);
        ExecuteDig(data, digAddress);

        // Note. 分岐の作成
        for (int index = 0; index < _callbackList.Count; index++)
        {
            _errorCount = 0;
            ExecuteDig(data, _callbackList[index]);
        }

        OnView(data);
    }

    /// <summary>
    /// 穴掘りの開始時点を設定
    /// </summary>
    /// <param name="data"></param>
    /// <returns>穴をあけた番地</returns>
    Vector2Int StartDig(MapData data)
    {
        int wideIndex = Random.Range(1, data.WideSize);
        int heightIndex = Random.Range(1, data.HeightSize);

        // Collect. 要素数が奇数出なければ奇数に変換
        wideIndex = wideIndex % 2 != 0 ? wideIndex : wideIndex - 1;
        heightIndex = heightIndex % 2 != 0 ? heightIndex : heightIndex - 1;

        _cellDatas[wideIndex, heightIndex].CellType = CellType.Start;

        return new Vector2Int(wideIndex, heightIndex);
    }

    /// <summary>
    /// 穴掘りを再帰的に繰り返す
    /// </summary>
    /// <param name="data"></param>
    /// <param name="digAddress">現在番地</param>
    void ExecuteDig(MapData data, Vector2Int digAddress)
    {
        // 特定回数掘ることができなければ終了。
        if (_errorCount > DigOverflowCount) return;

        Vector2Int digDir = _digDir[Random.Range(0, _digDir.Length)];

        if (IsDigCheck(digDir * 2, digAddress))
        {
            digAddress += digDir * 2;

            // 分岐作成の為に現在番地の保存
            _callbackList.Add(digAddress);

            // Note. 2マス先分を掘る
            _cellDatas[digAddress.x, digAddress.y].CellType = CellType.Load;
            _cellDatas[digAddress.x - digDir.x, digAddress.y - digDir.y].CellType = CellType.Load;
        }
        else
        {
            // エラーカウントの加算
            _errorCount++;
        }

        ExecuteDig(data, digAddress);
    }

    /// <summary>
    /// 2マス先が掘れるかの成否
    /// </summary>
    /// <param name="digDir">掘る方向</param>
    /// <param name="digAddress">現在番地</param>
    /// <returns></returns>
    bool IsDigCheck(Vector2Int digDir, Vector2Int digAddress)
    {
        try
        {
            digAddress += digDir;
            CellType cellType = _cellDatas[digAddress.x, digAddress.y].CellType;
            return cellType == CellType.Wall;
        }
        catch { return false; }
    }

    /// <summary>
    /// マップの可視化
    /// 各Cell情報に対するPrefabの生成。
    /// </summary>
    /// <param name="data"></param>
    void OnView(MapData data)
    {
        for (int wideIndex = 0; wideIndex < data.WideSize; wideIndex++)
            for (int heightIndex = 0; heightIndex < data.HeightSize; heightIndex++)
            {
                CellType cellType = _cellDatas[wideIndex, heightIndex].CellType;
                GameObject cell = Object.Instantiate(data.GetTipData(cellType));
                cell.transform.position = new Vector2(wideIndex, heightIndex) * data.MapScale;
                cell.transform.localScale = Vector2.one * data.MapScale;
            }
    }

    /// <summary>
    /// 初期化。
    /// CellTypeをWallに設定
    /// </summary>
    /// <param name="data"></param>
    void Initalize(MapData data)
    {
        for (int wideIndex = 0; wideIndex < data.WideSize; wideIndex++)
            for (int heightIndex = 0; heightIndex < data.HeightSize; heightIndex++)
            {
                _cellDatas[wideIndex, heightIndex] = new CellData(wideIndex, heightIndex, data.MapScale);
            }
    }
}
