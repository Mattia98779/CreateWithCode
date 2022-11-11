using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Counter : MonoBehaviour
{
    public Text CounterText;
    public GameObject spherePrefabs;

    private int Count = 0;

    private void Start()
    {
        Count = 0;
        for (int i = 0; i < 40; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-3, 3), Random.Range(10, 16), Random.Range(-3, 3));
            Instantiate(spherePrefabs,randomPosition,spherePrefabs.transform.rotation );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Count += 1;
        CounterText.text = "Count : " + Count;
    }
}
