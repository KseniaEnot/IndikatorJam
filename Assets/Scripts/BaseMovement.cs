using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BaseMovement : MonoBehaviour
{
    protected Tilemap groundTileMap;
    protected Tilemap collisionTileMap;
    const int times = 20;
    
    public void Initialize(TIlesManager tilesManager)
    {
        groundTileMap = tilesManager.groundTileMap;
        collisionTileMap = tilesManager.collisionTileMap;
    }

    protected IEnumerator MoveCoroutine(Transform moveTransform, Vector3 direction, Action endFunction)
    {
        for(int i = 0; i < times; i++)
        {
            moveTransform.position += direction / times;
            yield return new WaitForSeconds(0.01f);
        }
        
        endFunction();
    }

    protected IEnumerator AttackCoroutine(float seconds, Action endFunction)
    {
        yield return new WaitForSeconds(seconds);

        endFunction();
    }
}
