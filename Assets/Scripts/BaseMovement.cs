using System;
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

    protected IEnumerator MoveCoroutine(Vector3 direction, Action endFunction)
    {
        const int times = 20;
        for(int i = 0; i < times; i++)
        {
            transform.position += direction / times;
            yield return new WaitForSeconds(0.01f);
        }
        
        endFunction();
    }
}
