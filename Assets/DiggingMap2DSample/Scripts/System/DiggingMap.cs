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
    /// ���@��@�̊J�n
    /// </summary>
    /// <param name="data"></param>
    public void Create(MapData data)
    {
        _cellDatas = new CellData[data.WideSize, data.HeightSize];

        Initalize(data);

        Vector2Int digAddress = StartDig(data);
        ExecuteDig(data, digAddress);

        // Note. ����̍쐬
        for (int index = 0; index < _callbackList.Count; index++)
        {
            _errorCount = 0;
            ExecuteDig(data, _callbackList[index]);
        }

        OnView(data);
    }

    /// <summary>
    /// ���@��̊J�n���_��ݒ�
    /// </summary>
    /// <param name="data"></param>
    /// <returns>�����������Ԓn</returns>
    Vector2Int StartDig(MapData data)
    {
        int wideIndex = Random.Range(1, data.WideSize);
        int heightIndex = Random.Range(1, data.HeightSize);

        // Collect. �v�f������o�Ȃ���Ί�ɕϊ�
        wideIndex = wideIndex % 2 != 0 ? wideIndex : wideIndex - 1;
        heightIndex = heightIndex % 2 != 0 ? heightIndex : heightIndex - 1;

        _cellDatas[wideIndex, heightIndex].CellType = CellType.Start;

        return new Vector2Int(wideIndex, heightIndex);
    }

    /// <summary>
    /// ���@����ċA�I�ɌJ��Ԃ�
    /// </summary>
    /// <param name="data"></param>
    /// <param name="digAddress">���ݔԒn</param>
    void ExecuteDig(MapData data, Vector2Int digAddress)
    {
        // ����񐔌@�邱�Ƃ��ł��Ȃ���ΏI���B
        if (_errorCount > DigOverflowCount) return;

        Vector2Int digDir = _digDir[Random.Range(0, _digDir.Length)];

        if (IsDigCheck(digDir * 2, digAddress))
        {
            digAddress += digDir * 2;

            // ����쐬�ׂ̈Ɍ��ݔԒn�̕ۑ�
            _callbackList.Add(digAddress);

            // Note. 2�}�X�敪���@��
            _cellDatas[digAddress.x, digAddress.y].CellType = CellType.Load;
            _cellDatas[digAddress.x - digDir.x, digAddress.y - digDir.y].CellType = CellType.Load;
        }
        else
        {
            // �G���[�J�E���g�̉��Z
            _errorCount++;
        }

        ExecuteDig(data, digAddress);
    }

    /// <summary>
    /// 2�}�X�悪�@��邩�̐���
    /// </summary>
    /// <param name="digDir">�@�����</param>
    /// <param name="digAddress">���ݔԒn</param>
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
    /// �}�b�v�̉���
    /// �eCell���ɑ΂���Prefab�̐����B
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
    /// �������B
    /// CellType��Wall�ɐݒ�
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
