using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField] private float speed;
    [SerializeField] private float horsePower = 0;
    [SerializeField] private float rpm;
    private float turnSpeed = 35;
    private float horizontalInput;
    private float forwardInput;
    private Rigidbody playerRb;
    [SerializeField] private GameObject centerOfMass;
    [SerializeField] private TextMeshProUGUI speedometerText;
    [SerializeField] private TextMeshProUGUI rpmText;

    [SerializeField] private List<WheelCollider> allWheels;
    [SerializeField] private int wheelOnGround;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Move the car
        //transform.Translate(Vector3.forward*Time.deltaTime*speed*forwardInput);
        if (isOnGround())
        {
            playerRb.AddRelativeForce(Vector3.forward * horsePower * forwardInput);
            // Rotate the car
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.Round(playerRb.velocity.magnitude * 3.6f);
            speedometerText.SetText("speed: "+ speed +" Km/h");

            rpm = (speed % 30) * 40;
            rpmText.SetText("RPM: "+ speed );

        }
    }

    bool isOnGround()
    {
        wheelOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelOnGround++;
            }
        }

        return wheelOnGround == 4;
    }
}
