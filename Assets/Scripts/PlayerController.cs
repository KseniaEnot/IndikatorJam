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

    private bool canMove = true;

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
        characterAnimator.SetBool("IsWalk", false);
        if (flower != null)
        {
            characterAnimator.SetBool("IsAtack", true);
            canMove = false;
            StartCoroutine(AttackCoroutine(1.5f, () => 
            {
                flower.GetComponentInChildren<Flower>().gameObject.SetActive(false);
                characterAnimator.SetBool("IsAtack", false); 
                flowerCollection.GetFlower(flower);
                canMove = true;
                moveRandomizer.HandleMovement();
            }));
            return;
        }
        moveRandomizer.HandleMovement();
    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition) || !moveRandomizer.CanMove() && canMove){
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
