using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    private Vector3 spawnPos = new Vector3(25,0,0);

    private float startDelay = 2;
    private float repeatRate = 2;

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameOver==false)
        {
            int randomIndex = Random.Range(0, 2);
            if (randomIndex==0)
            {
                Instantiate(obstaclePrefab[randomIndex], spawnPos, obstaclePrefab[randomIndex].transform.rotation);
                Instantiate(obstaclePrefab[randomIndex], spawnPos+new Vector3(0,2,0), obstaclePrefab[randomIndex].transform.rotation);
                Instantiate(obstaclePrefab[randomIndex], spawnPos+new Vector3(0,4,0), obstaclePrefab[randomIndex].transform.rotation);
            }
            else
            {
                Instantiate(obstaclePrefab[randomIndex], spawnPos, obstaclePrefab[randomIndex].transform.rotation);
            }
            
        }
    }
    
}
