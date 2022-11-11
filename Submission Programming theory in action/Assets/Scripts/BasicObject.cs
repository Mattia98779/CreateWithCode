using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicObject : MonoBehaviour
{
    protected float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    //abstraction
    public virtual void Move(){
        transform.Translate(Vector3.up* Time.deltaTime * speed);
    }

    //encapsulation
    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getSpeed()
    {
        return speed;
    }
}
