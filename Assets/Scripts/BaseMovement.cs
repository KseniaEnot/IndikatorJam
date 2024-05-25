using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseMovement : MonoBehaviour
{
    protected Tilemap groundTileMap;
    protected Tilemap collisionTileMap;
    
    public void Initialize(TIlesManager tilesManager)
    {
        groundTileMap = tilesManager.groundTileMap;
        collisionTileMap = tilesManager.collisionTileMap;
    }
}
