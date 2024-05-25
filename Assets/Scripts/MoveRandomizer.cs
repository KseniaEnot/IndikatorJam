using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomizer : MonoBehaviour
{
    private int movementLeft = 0;

    public bool CanMove() => movementLeft > 0;

    public void GenerateNew()
    {
        movementLeft = Random.Range(1, 4);
        Debug.Log("Rolled " + movementLeft);
    }

    public void HandleMovement()
    {
        movementLeft--;
        if (movementLeft == 0)
        {
            Debug.Log("Закончилась ходьба");
        }
    }
}
