using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : BaseMovement
{
    public Animator characterAnimator;
    [SerializeField] private MoveRandomizer moveRandomizer;

    public Action playerMoved = () => {};

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
        playerMoved.Invoke();
        characterAnimator.SetBool("IsWalk", false);
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition) || !moveRandomizer.CanMove()){
            return false;
        }

        return true;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<Flower>(out var flower))
        {
            flowerCollection.GetFlower(flower);
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
