using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private GameManager gameManager;
    public bool needGrow = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snake") || other.gameObject.CompareTag("Wall"))
        {
            gameManager.GameOver();
        }
        else if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            gameManager.AddScore(5);
            needGrow = true;
        }
    }

}
