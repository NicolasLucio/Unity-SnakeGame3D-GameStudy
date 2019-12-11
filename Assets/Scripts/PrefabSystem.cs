using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSystem : MonoBehaviour
{
    public GameObject foodPrebab;
    private GameObject[] tailArray;

    public GameObject wallRight;
    public GameObject wallLeft;
    public GameObject wallTop;
    public GameObject wallBot;

    private Vector3 wallRightPosition;
    private Vector3 wallLeftPosition;
    private Vector3 wallTopPosition;
    private Vector3 wallBotPosition;

    private float spawnTimerInSeconds;
    public float difficultySecondsChange;

    private bool tailExist;
    
    void Start()
    {
        spawnTimerInSeconds = 5.0f;
        difficultySecondsChange = 10.0f;
        StartCoroutine(SpawnTimer());
        StartCoroutine(DifficultyTimer());                
    }    

    void TakeVector()
    {
        wallRightPosition = wallRight.transform.position;
        wallLeftPosition = wallLeft.transform.position;
        wallTopPosition = wallTop.transform.position;
        wallBotPosition = wallBot.transform.position;
    }

    void Spawn()
    {
        TakeVector();
        tailExist = false;

        int posX = (int)Random.Range(wallLeftPosition.x + 1, wallRightPosition.x - 1);
        int posY = (int)Random.Range(wallBotPosition.y + 1, wallTopPosition.y - 1);
        
        Vector3 instantiateVector = new Vector3(posX, posY, 0.0f);
        tailArray = GameObject.FindGameObjectsWithTag("Tail");

        if (tailArray.Length != 0)
        {
            foreach (GameObject aux in tailArray)
            {
                if (aux.transform.position == instantiateVector)
                {
                    tailExist = true;
                }
            }
        }        

        if (tailExist == true)
        {
            Spawn();
        }
        else if (tailExist == false)
        {
            Instantiate(foodPrebab, instantiateVector, Quaternion.identity);
        }        
    }

    void MoveWalls()
    {
        wallRight.transform.DOMoveX(1.0f, 600.0f, false);
        wallLeft.transform.DOMoveX(-1.0f, 600.0f, false);
        wallTop.transform.DOMoveY(1.0f, 600.0f, false);
        wallBot.transform.DOMoveY(-1.0f, 600.0f, false);
    }

    IEnumerator SpawnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimerInSeconds);
            Spawn();
        }
    }

    IEnumerator DifficultyTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(difficultySecondsChange);
            spawnTimerInSeconds = spawnTimerInSeconds * 0.9f;
            MoveWalls();
        }
    }
}
