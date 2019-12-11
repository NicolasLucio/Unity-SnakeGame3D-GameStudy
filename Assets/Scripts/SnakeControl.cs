using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class SnakeControl : MonoBehaviour
{
    private List<Transform> tail = new List<Transform>();

    private bool boolEat = false;
    public GameObject tailPrefab;
    private Vector3 direction = Vector3.right;
    private float moveTimer;

    public float secondsForModifier;
    public int scoreNumber;
    private int modifier;

    private bool playerIsAlive;
    public TextMeshProUGUI gameOverText;

    public PostProcessVolume snakePPVolume;
    
    void Start()
    {
        playerIsAlive = true;
        moveTimer = 0.3f;
        scoreNumber = 0;
        secondsForModifier = 10.0f;
        modifier = 1;
        gameOverText.text = "";
        snakePPVolume.enabled = false;
        StartCoroutine(ControlTimer());
        StartCoroutine(DifficultyModifier());
    }
    
    void FixedUpdate()
    {
        if (playerIsAlive == true)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                direction = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                direction = Vector3.down;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                direction = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                direction = Vector3.up;
            }
        }        
    }

    void Move()
    {
        Vector3 actualPosition = transform.position;
        transform.Translate(direction);
        if (boolEat == true)
        {
            GameObject newTailElement = (GameObject)Instantiate(tailPrefab, actualPosition, Quaternion.identity);
            tail.Insert(0, newTailElement.transform);
            boolEat = false;
        }
        else if (tail.Count > 0) 
        {
            tail.Last().position = actualPosition;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }       
    }

    IEnumerator ControlTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(moveTimer);
            Move();
        }
    }

    IEnumerator DifficultyModifier()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsForModifier);
            moveTimer = moveTimer * 0.9f;
            modifier++;
        }
    }

    IEnumerator GameOver()
    {
        gameOverText.text = "Wasted";
        snakePPVolume.enabled = true;
        playerIsAlive = false;
        direction = Vector3.zero;
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Game");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            boolEat = true;
            scoreNumber = scoreNumber + modifier;            
            Destroy(other.gameObject);
        }
        if (other.tag == "Wall" || other.tag == "Tail")
        {
            StartCoroutine(GameOver());            
        }
    }
}
