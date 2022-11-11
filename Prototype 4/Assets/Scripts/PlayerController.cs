using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float powerupStrength = 15;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public bool hasPowerup;
    public GameObject missilePrefab;
    private GameObject tmprocket;
    public bool isOnGround = true;
    public bool smashing = false;

    public GameObject powerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position+ new Vector3(0,-0.5f,0);
        if (hasPowerup && Input.GetKeyDown(KeyCode.F))
        {
            LunchRocket();
        }

        if (Input.GetKeyDown(KeyCode.E) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * 500);
            isOnGround = false;
        }else if (hasPowerup && Input.GetKeyDown(KeyCode.E) && isOnGround==false)
        {
            playerRb.AddForce(Vector3.down * 50, ForceMode.Impulse);
            smashing = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("colpito nemico con powerUp!");
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            if (smashing)
            {
                foreach (var enemy in FindObjectsOfType<Enemy>())
                {
                    Rigidbody enemyRb = enemy.gameObject.GetComponent<Rigidbody>();
                    Vector3 awayFromPlayer = (enemy.gameObject.transform.position - transform.position);
                    if (awayFromPlayer.magnitude<5)
                    {
                        enemyRb.AddForce(awayFromPlayer * (powerupStrength/2), ForceMode.Impulse);
                    }
                }
            }
        }
    }

    void LunchRocket()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmprocket = Instantiate(missilePrefab, transform.position +Vector3.up, Quaternion.identity);
            tmprocket.GetComponent<HomingMissile>().Fire(enemy.transform);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    
}
