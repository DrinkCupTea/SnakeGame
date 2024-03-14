using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float topMargin;
    [SerializeField] private float bottomMargin;
    [SerializeField] private float leftMargin;
    [SerializeField] private float rightMargin;
    [SerializeField] private float thickness;

    public float topWallY;
    public float bottomWallY;
    public float leftWallX;
    public float rightWallX;
    private Snake snake;

    void Awake()
    {
        snake = FindObjectOfType<Snake>().GetComponent<Snake>();
        CreateWalls();
    }

    void CreateWalls()
    {
        CalWallPosition();

        float horizontalCenter = (bottomMargin - topMargin) / 2;
        float verticalCenter = (rightMargin - leftMargin) / 2;
        float width = rightWallX - leftWallX + thickness;
        float height = topWallY - bottomWallY;

        CreateWall(new Vector3(verticalCenter, topWallY, 0), new Vector3(width, thickness, 1));
        CreateWall(new Vector3(verticalCenter, bottomWallY, 0), new Vector3(width, thickness, 1));
        CreateWall(new Vector3(leftWallX, horizontalCenter, 0), new Vector3(thickness, height, 1));
        CreateWall(new Vector3(rightWallX, horizontalCenter, 0), new Vector3(thickness, height, 1));
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

    void CalWallPosition()
    {
        float halfScreenHeight = Camera.main.orthographicSize;
        float halfScreenWidth = halfScreenHeight * Camera.main.aspect;

        bottomWallY = -halfScreenHeight + bottomMargin;
        topWallY = halfScreenHeight - topMargin;
        leftWallX = -halfScreenWidth + leftMargin;
        rightWallX = halfScreenWidth - rightMargin;

        float segmentSize = snake.GetSnakeSegmentSize();

        topMargin += topWallY % segmentSize;
        bottomMargin += Mathf.Abs(bottomWallY) % segmentSize;
        leftMargin += Mathf.Abs(leftWallX) % segmentSize;
        rightMargin += rightWallX % segmentSize;

        topWallY -= topWallY % segmentSize;
        bottomWallY += Mathf.Abs(bottomWallY) % segmentSize;
        leftWallX += Mathf.Abs(leftWallX) % segmentSize;
        rightWallX -= rightWallX % segmentSize;
    }
}
