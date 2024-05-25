using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : BaseMovement
{
    public Animator characterAnimator;
    public MoveRandomizer moveRandomizer;

    private Controls inputActions;
    private FlowerCollection flowerCollection;

    private GameObject flower = null;

    void Awake()
    {
        inputActions = new Controls();
        flowerCollection = gameObject.GetComponent<FlowerCollection>();
    }

    void Start()
    {
        inputActions.Main.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        inputActions.Main.Roll.performed += ctx => Roll();
    }

    private void Roll()
    {
        if (moveRandomizer.CanMove())
        {
            return;
        }
        moveRandomizer.GenerateNew();
    }

    private void Move(Vector2 direction)
    {
        if (CanMove(direction)){
            characterAnimator.SetBool("IsWalk", true);
            StartCoroutine(MoveCoroutine((Vector3)direction, OnMoveEnd));
        }
    }

    private void OnMoveEnd()
    {
        moveRandomizer.HandleMovement();
        characterAnimator.SetBool("IsWalk", false);
        if (flower != null)
        {
            flowerCollection.GetFlower(flower);
        }
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition) || !moveRandomizer.CanMove()){
            return false;
        }

        return true;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Olayer entered " + other.name);
        if (other.tag == "Flower")
        {
            flower = other.gameObject;
        }
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
