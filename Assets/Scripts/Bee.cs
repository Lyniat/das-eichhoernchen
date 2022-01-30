using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2D.position = Midpoint.position;
        var rotPos = new Vector2(Mathf.Sin(Time.fixedTime * Speed)*RadX, Mathf.Cos(Time.fixedTime * Speed)*RadY);
        rigidbody2D.SetRotation(Mathf.Atan2(rotPos.y, rotPos.x) * Mathf.Rad2Deg);
        rigidbody2D.position += rotPos;
    }
}
