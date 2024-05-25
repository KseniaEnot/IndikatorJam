using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlowerCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flowerText;

    private int count = 0;

    void Awake()
    {
        count = 0;
        flowerText.text = count.ToString();
    }

    public void GetFlower(GameObject flower)
    {
        Destroy(flower);
        count++;
        flowerText.text = count.ToString();
    }
}
