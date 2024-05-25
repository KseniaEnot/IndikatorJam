using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Flower : BaseMovement
{
    private GameObject player;
    public Animator characterAnimator;

    private readonly List<Vector3Int> directions = new List<Vector3Int>(){
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 1, 0)
    };

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter " + other.name);
        if (other.tag == "Player")
        {
            var playerController = other.GetComponent<PlayerController>();
            player = other.gameObject;
            playerController.moveRandomizer.movementFinished += OnPlayerMove;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var playerController = other.GetComponent<PlayerController>();
            playerController.moveRandomizer.movementFinished -= OnPlayerMove;
        }
    }

    void OnDisable()
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.moveRandomizer.movementFinished -= OnPlayerMove;
        player = null;
    }

    private void OnPlayerMove()
    {
        // Move
        var playerPosition = player.transform.position;
        var flowerPosition = transform.position;

        var distance = flowerPosition - playerPosition;
        Vector3Int? bestDirection = null;
        float maxMagnitude = 0;
        foreach(var direction in directions)
        {
            if (!CanMove(direction))
            {
                continue;
            }
            var magnitude = (distance + direction).magnitude;
            if (magnitude > maxMagnitude) { maxMagnitude = magnitude; bestDirection = direction; }
        }

        if (bestDirection != null)
        {
            transform.parent.transform.position += (Vector3)bestDirection;        
        }
    }
    
    private bool CanMove(Vector3Int direction)
    {
        Vector3Int gridPosition = groundTileMap.WorldToCell(transform.position + direction);
        if (!groundTileMap.HasTile(gridPosition) || collisionTileMap.HasTile(gridPosition)){
            characterAnimator.SetBool("IsWalk", true);
            StartCoroutine(MoveCoroutine((Vector3)direction, OnMoveEnd));
            return false;
        }

        return true;
    }

    private void OnMoveEnd()
    {
        characterAnimator.SetBool("IsWalk", false);
    }
}
