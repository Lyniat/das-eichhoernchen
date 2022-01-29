using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawArm : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D start;
    [SerializeField]
    private Rigidbody2D end;

    private LineRenderer line;
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.positionCount = 2;
        line.SetPosition(0,start.position);
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.SetPosition(1,end.position);
    }
}
