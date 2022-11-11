using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = true;
    public bool canDoubleJump;
    public int sprinting = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<0)
        {
            transform.Translate(Vector3.forward*Time.deltaTime);
            dirtParticle.Stop();

        }else if (transform.position.x > 0 && gameOver)
        {
            gameOver = false;
            playerAnim.SetFloat("Speed_f",1);
            transform.position = new Vector3(0,0,0);
            dirtParticle.Play();
        }else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound,1);
            }else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !gameOver)
            {
                playerRb.AddForce(Vector3.up * (jumpForce * 0.5f), ForceMode.Impulse);
                canDoubleJump = false;
                playerAudio.PlayOneShot(jumpSound,1);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = 2;
                playerAnim.speed = 2;
            }
            else
            {
                sprinting = 1;
                playerAnim.speed = 1;
            }
        }
        
        
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            canDoubleJump = true;
            dirtParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            Debug.Log("gameOver");
            gameOver = true;
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound,1);
        }
    }
}
