using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Bee : MonoBehaviour
{
    [SerializeField]
    private Transform Midpoint;
    [SerializeField]
    private float RadX;
    [SerializeField]
    private float RadY;

    [SerializeField]
    private float Speed;
    
    [SerializeField]
    private float UpSpeed;

    private Rigidbody2D rigidbody2D;

    private float up;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.position = Midpoint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        up += UpSpeed * Time.fixedDeltaTime;
        //rigidbody2D.position = Midpoint.position;
        var rotPos = new Vector2(Mathf.Sin(Time.fixedTime * Speed)*RadX, Mathf.Cos(Time.fixedTime * Speed)*RadY);
        rigidbody2D.SetRotation(Mathf.Atan2(rotPos.y, rotPos.x) * Mathf.Rad2Deg);
        rigidbody2D.position = rotPos + new Vector2(0,up);
    }
}
