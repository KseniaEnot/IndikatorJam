using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlowerCollection : MonoBehaviour
{
    [SerializeField] private Tilemap flowerTileMap;
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

    public void CheckFlowers()
    {
        Vector3Int gridPosition = flowerTileMap.WorldToCell(transform.position);
        foreach(var tile in directions)
        {
            if (flowerTileMap.HasTile(gridPosition + tile))
            {
                GetFlower(gridPosition + tile);
            }
        }
    }

    public void GetFlower(Vector3Int position)
    {
        Debug.Log("Get Tile " + position);
        flowerTileMap.SetTile(position, null);
        count++;
        flowerText.text = count.ToString();
    }
}
