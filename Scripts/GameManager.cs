using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool hasGameStarted = false;
    public TextMeshProUGUI pressStart;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public bool gameOver;
    int score = 0;
    private void Start()
    {
        pressStart.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            hasGameStarted = true;
            pressStart.gameObject.SetActive(false);
        }
        if(gameOver)
        {
            gameOverText.gameObject.SetActive(true);
        }
        
        if (!gameOver && hasGameStarted)
        {
            score = score + (int)((Time.deltaTime * 1f) + 1f);            
        }
        scoreText.text = "Score : " + score;
    }
}
