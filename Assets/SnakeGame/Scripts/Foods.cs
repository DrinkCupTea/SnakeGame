using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private int maxFoodNum;
    private GameManager gameManager;
    private float step;
    private int horizontalRange;
    private int verticalRange;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        Snake snake = FindObjectOfType<Snake>().GetComponent<Snake>();
        step = snake.GetSnakeSegmentSize() * 1.2f;

        float screenHeight = Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;
        horizontalRange = (int)(screenWidth / step);
        verticalRange = (int)(screenHeight / step);

        StartCoroutine(nameof(GenerateFood));
    }

    IEnumerator GenerateFood()
    {
        while (!gameManager.gameOver)
        {
            yield return new WaitForSeconds(Random.Range(2, 6));
            int foodsNum = GameObject.FindGameObjectsWithTag("Food").Length;
            if (foodsNum >= maxFoodNum)
            {
                continue;
            }
            float x = Random.Range(-horizontalRange, horizontalRange) * step;
            float y = Random.Range(-verticalRange, verticalRange) * step;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
            newFood.transform.parent = transform;
        }
    }

}
