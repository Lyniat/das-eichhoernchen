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

    [SerializeField]
    private int LineCount;
    
    [SerializeField]
    private float SinMultiplier;
    [SerializeField]
    private float SinLength;
    [SerializeField]
    private float SinTime;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = LineCount;
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < LineCount; i++)
        {
            var sin = Mathf.Sin(Time.time*SinTime + i*SinLength) * SinMultiplier;
            var posX = Mathf.Lerp(start.position.x, end.position.x, i / (float) LineCount);
            var posY = Mathf.Lerp(start.position.y, end.position.y, i / (float) LineCount);
            line.SetPosition(i,new Vector3(posX + sin,posY,0));
        }
        //line.SetPosition(0,start.position);
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        //line.SetPosition(1,end.position);
    }
}
