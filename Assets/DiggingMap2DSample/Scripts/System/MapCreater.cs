using UnityEngine;

public class MapCreater : MonoBehaviour
{
    [SerializeField] MapData _mapData;

    DiggingMap _diggingMap = new DiggingMap();

    void Start()
    {
        _diggingMap.Create(_mapData);
    }
}
