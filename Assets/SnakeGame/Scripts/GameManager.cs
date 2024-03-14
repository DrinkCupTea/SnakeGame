using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        gameOver = true;
    }
}
