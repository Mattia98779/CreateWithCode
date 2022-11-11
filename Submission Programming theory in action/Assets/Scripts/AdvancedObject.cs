using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//inheritance
public class AdvancedObject : BasicObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //polymorphism
    public override void Move()
    {
        
        transform.Translate(Vector3.down* Time.deltaTime * speed);
    }
}
