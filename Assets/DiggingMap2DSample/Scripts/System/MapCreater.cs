using UnityEngine;

/// <summary>
/// Map�̐����N���X
/// </summary>

public class MapCreater : MonoBehaviour
{
    [SerializeField] Transform _parent;
    [SerializeField] MapData _mapData;

    DiggingMap _diggingMap = new DiggingMap();

    void Start()
    {
        // Note. �e�����Ȃ���Ύ��g��e�ɂ���B
        if (_parent == null)
        {
            _parent = transform;
        }

        _diggingMap.Create(_mapData);
        _diggingMap.SetParent(_parent);
    }
}
