using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Snake snake;

    // private GameManager gameManager;
    void Start()
    {
        snake = FindObjectOfType<Snake>().GetComponent<Snake>();
        CreateWalls();
    }

    void CreateWalls()
    {
        float screenHeight = Camera.main.orthographicSize;
        float screenWidth = screenHeight * Camera.main.aspect;
        float thickness = screenHeight % (snake.GetSnakeSegmentSize() * 1.2f);
        if (thickness < 0.3)
        {
            thickness += snake.GetSnakeSegmentSize() * 1.2f;
        }
        Debug.Log("screenHeight: " + screenHeight + "thickness: " + thickness + "Prefab Size: " + snake.GetSnakeSegmentSize() * 1.2);

        CreateWall(new Vector3(0, screenHeight, 0), new Vector3(screenWidth * 2f, thickness, 1));
        CreateWall(new Vector3(0, -screenHeight, 0), new Vector3(screenWidth * 2f, thickness, 1));
        CreateWall(new Vector3(-screenWidth, 0, 0), new Vector3(thickness, screenHeight * 2f, 1));
        CreateWall(new Vector3(screenWidth, 0, 0), new Vector3(thickness, screenHeight * 2f, 1));
    }

    GameObject CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.parent = transform;
        wall.transform.localScale = scale;
        wall.transform.position = position;
        wall.tag = "Wall";
        return wall;
    }
}
