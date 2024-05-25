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
    private FlowerCollection flowerCollection;

    void Awake()
    {
        inputActions = new Controls();
        flowerCollection = gameObject.GetComponent<FlowerCollection>();
    }

    void Start()
    {
        inputActions.Main.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction)){
            transform.position += (Vector3)direction;
            flowerCollection.CheckFlowers();
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition)){
            return false;
        }

        return true;
    }
    
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
