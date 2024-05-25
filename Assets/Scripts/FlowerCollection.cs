using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlowerCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI flowerText;
    [SerializeField] private int finalSceneIndex;

    private int count = 0;
    private int maxCount;

    void Awake()
    {
        count = 0;
        maxCount = GameObject.FindGameObjectsWithTag("Flower").Count();
        flowerText.text = $"{count}/{maxCount}";
    }

    public void GetFlower(GameObject flower)
    {
        Destroy(flower);
        count++;
        flowerText.text = $"{count}/{maxCount}";

        if (count == maxCount)
        {
            SceneManager.LoadScene(finalSceneIndex);
        }
    }
}
