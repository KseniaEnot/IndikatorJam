using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Tilemap groundTileMap;
    [SerializeField] private Tilemap collisionTileMap;
    public Animator characterAnimator;
    [SerializeField] private MoveRandomizer moveRandomizer;

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
            transform.position += (Vector3)direction;
            flowerCollection.CheckFlowers();
            moveRandomizer.HandleMovement();
            //characterAnimator.SetBool("IsWalk", false);
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
    
    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
