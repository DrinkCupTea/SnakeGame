using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Snake : MonoBehaviour
{
    [SerializeField] private float moveRate;
    [SerializeField] private GameObject snakeSegmentPrefab;
    [SerializeField] private float scale = 1.2f;
    private readonly List<GameObject> snakeSegments = new();
    private Vector3 lastMoveDirection = Vector3.left;
    private Vector3 headDirection = Vector3.left;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        float segmentSize = snakeSegmentPrefab.transform.localScale.x;
        AddSegment(Vector3.zero);
        snakeSegments[0].AddComponent<SnakeHead>();
        AddSegment(new Vector3(segmentSize * scale, 0, 0));
        AddSegment(new Vector3(segmentSize * 2 * scale, 0, 0));

        StartCoroutine(nameof(Move));
    }


    // Update is called once per frame
    void Update()
    {
        UpdateHeadDirection();
    }

    void AddSegment(Vector3 pos)
    {
        GameObject newSegment = Instantiate(snakeSegmentPrefab, pos, Quaternion.identity);
        newSegment.transform.parent = transform;
        snakeSegments.Add(newSegment);
    }
    public float GetSnakeSegmentSize()
    {
        return snakeSegmentPrefab.transform.localScale.x * scale;
    }

    void UpdateHeadDirection()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && lastMoveDirection != Vector3.down)
        {
            headDirection = Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && lastMoveDirection != Vector3.up)
        {
            headDirection = Vector3.down;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastMoveDirection != Vector3.right)
        {
            headDirection = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && lastMoveDirection != Vector3.left)
        {
            headDirection = Vector3.right;
        }
    }

    void MoveSnakeHead()
    {
        snakeSegments[0].transform.position += headDirection * (snakeSegments[0].transform.localScale.x * 1.2f);
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveRate);
            if (gameManager.gameOver)
            {
                break;
            }
            Vector3 lastEndSegmentPos = snakeSegments[^1].transform.position;
            Vector3 previousSegmentPos = snakeSegments[0].transform.position;
            MoveSnakeHead();
            for (int i = 1; i < snakeSegments.Count; i++)
            {
                (previousSegmentPos, snakeSegments[i].transform.position) = (snakeSegments[i].transform.position, previousSegmentPos);
            }
            if (snakeSegments[0].GetComponent<SnakeHead>().needGrow)
            {
                AddSegment(lastEndSegmentPos);
                snakeSegments[0].GetComponent<SnakeHead>().needGrow = false;
            }
            lastMoveDirection = headDirection;
        }
    }
}
