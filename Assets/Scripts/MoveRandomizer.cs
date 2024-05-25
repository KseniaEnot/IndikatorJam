using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoveRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> diceSpries;
    [SerializeField] private Image dice;
    [SerializeField] private GameObject TextSpace;
    [Header("Randomizer Setting")]
    [SerializeField] private int timeToRandomize = 10;
    [SerializeField] private float delayTime = 0.1f;

    public Action movementFinished = () => {};

    private int movementLeft = 0;

    void Awake()
    {
        dice.gameObject.SetActive(false);
    }

    public bool CanMove() => movementLeft > 0;

    public void GenerateNew()
    {
        dice.gameObject.SetActive(true);
        TextSpace.SetActive(false);
        StartCoroutine(RollTheDice());
    }

    public void HandleMovement()
    {
        movementLeft--;
        if (movementLeft == 0)
        {
            dice.gameObject.SetActive(false);
            TextSpace.SetActive(true);
            movementFinished.Invoke();
        }
        else
        {
            dice.sprite = diceSpries[movementLeft - 1];
        }
    }

    private IEnumerator RollTheDice()
    {
        int side = 0;

        for(int i = 0; i < timeToRandomize; i++)
        {
            side = UnityEngine.Random.Range(0, 4);
            dice.sprite = diceSpries[side];
            yield return new WaitForSeconds(delayTime);
        }

        movementLeft = side + 1;
        Debug.Log("Rolled " + movementLeft);
    }
}
