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

    private int timerInSeconds;

    void Start()
    {
        timerInSeconds = 0;
        snakeScript = snakeObject.GetComponent<SnakeControl>();
        timerText.text = timerInSeconds.ToString();
        StartCoroutine(SystemTimer());
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
