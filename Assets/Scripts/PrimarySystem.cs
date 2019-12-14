using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrimarySystem : MonoBehaviour
{
    public GameObject snakeObject;
    public SnakeControl snakeScript;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI snakeGameText;

    private int timerInSeconds;
    private bool paused;

    void Start()
    {
        paused = false;
        timerInSeconds = 0;
        snakeScript = snakeObject.GetComponent<SnakeControl>();
        timerText.text = timerInSeconds.ToString();
        StartCoroutine(SystemTimer());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            if (paused == false)
            {
                Time.timeScale = 0.0f;
                paused = true;
                snakeGameText.text = "PAUSED";
            }
            else if (paused == true)
            {
                Time.timeScale = 1.0f;
                paused = false;
                snakeGameText.text = "SNAKE GAME";
            }            
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        scoreText.text = "Score: " + snakeScript.scoreNumber.ToString();
    }

    IEnumerator SystemTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            timerInSeconds++;
            timerText.text = timerInSeconds.ToString();
        }
    }
}
