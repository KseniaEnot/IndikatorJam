using UnityEngine;
using UnityEngine.Tilemaps;

public class TIlesManager : MonoBehaviour
{
    public Tilemap groundTileMap;
    public Tilemap collisionTileMap;

    void Awake()
    {
        var components = FindObjectsByType<BaseMovement>(FindObjectsSortMode.None);
        foreach(var component in components)
        {
            component.Initialize(this);
        }
    }
}
