using UnityEngine;

/// <summary>
/// Mapの生成クラス
/// </summary>

public class MapCreater : MonoBehaviour
{
    [SerializeField] Transform _parent;
    [SerializeField] MapData _mapData;

    DiggingMap _diggingMap = new DiggingMap();

    void Start()
    {
        // Note. 親がいなければ自身を親にする。
        if (_parent == null)
        {
            _parent = transform;
        }

        _diggingMap.Create(_mapData);
        _diggingMap.SetParent(_parent);
    }
}
