using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�b�v�𐶐����邽�߂̃f�[�^�N���X
/// </summary>

[System.Serializable]
public class MapData
{
    /// <summary>
    /// �eCellType�ɑ΂���Prefab�̃f�[�^
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

    // Collect. �v�f������o�Ȃ���Ί�ɕϊ�
    public int WideSize => _wideSize % 2 != 0 ? _wideSize : _wideSize + 1;
    public int HeightSize => _heightSize % 2 != 0 ? _heightSize : _heightSize + 1;

    public int MapScale => Mathf.Abs(_mapScale);

    /// <summary>
    /// ����̃}�b�v�`�b�v�̎擾
    /// </summary>
    /// <param name="cellType"></param>
    /// <returns>GameObjectPrefab</returns>
    public GameObject GetTipData(CellType cellType)
    {
        return _tipDataList.Find(t => t.CellType == cellType).TipPrefab;
    }
}
