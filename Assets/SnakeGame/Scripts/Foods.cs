using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private int maxFoodNum;
    private GameManager gameManager;
    private float step;
    public int topRange;
    public int bottomRange;
    public int leftRange;
    public int rightRange;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        Snake snake = FindObjectOfType<Snake>().GetComponent<Snake>();
        step = snake.GetSnakeSegmentSize();

        Wall wall = FindObjectOfType<Wall>().GetComponent<Wall>();
        topRange = (int)(wall.topWallY / step);
        bottomRange = (int)(wall.bottomWallY / step);
        leftRange = (int)(wall.leftWallX / step);
        rightRange = (int)(wall.rightWallX / step);

        StartCoroutine(nameof(GenerateFood));
    }

    IEnumerator GenerateFood()
    {
        while (!gameManager.gameOver)
        {
            int foodsNum = GameObject.FindGameObjectsWithTag("Food").Length;
            if (foodsNum >= maxFoodNum)
            {
                continue;
            }
            float x = Random.Range(leftRange, rightRange) * step;
            float y = Random.Range(bottomRange, topRange) * step;
            GameObject newFood = Instantiate(foodPrefab, new Vector3(x, y, 0), Quaternion.identity);
            newFood.transform.parent = transform;
            yield return new WaitForSeconds(Random.Range(2, 6));
        }
    }


}
