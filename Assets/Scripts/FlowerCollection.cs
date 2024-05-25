using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlowerCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flowerText;

    private int count = 0;

    private readonly List<Vector3Int> directions = new List<Vector3Int>(){
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 1, 0)
    };

    void Awake()
    {
        count = 0;
        flowerText.text = count.ToString();
    }

    public void GetFlower(Flower flower)
    {
        Destroy(flower.gameObject);
        count++;
        flowerText.text = count.ToString();
    }
}
