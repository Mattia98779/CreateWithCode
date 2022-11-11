using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float lastDogTime = 0;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastDogTime > 1.0f)
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                lastDogTime = Time.time;
            }

        }
    }
}
