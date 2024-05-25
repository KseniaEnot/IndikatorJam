using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap collisionTileMap;

    private Controls inputActions;

    void Awake()
    {
        inputActions = new Controls();
        Debug.Log("Actions");
    }

    void Start()
    {
        Debug.Log("STart");
        inputActions.Main.Move.performed += ctx => Move();
    }
    
    private void OnEnable()
    {
        Debug.Log("Enable");
        inputActions.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("Disable");
        inputActions.Disable();
    }

    private void Move()
    {
        Debug.Log("Move");
        //Debug.Log("Move " + direction);
        //if (CanMove(direction)){
        //    transform.position += (Vector3)direction;
        //}
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition)){
            return false;
        }

        return true;
    }
}
