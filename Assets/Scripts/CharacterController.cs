using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Body;
    [SerializeField]
    private HingeJoint2D LeftJoint;
    [SerializeField]
    private FixedJoint2D LeftArm;
    [SerializeField]
    private HingeJoint2D LeftHandJoint;
    
    
    [SerializeField]
    private HingeJoint2D RightJoint;
    [SerializeField]
    private FixedJoint2D RightArm;
    [SerializeField]
    private HingeJoint2D RightHandJoint;
    
    
    [SerializeField]
    private float Acceleration = 0f;

    private float leftArmLength = 1f;

    [SerializeField]
    public float LeftArmStartSpeeed;

    private float leftArmSpeed;
    private bool leftArmMoving = false;

    private float time = 0f;

    private Vector2 leftArmStart;
    private Vector2 rightArmStart;

    private float leftArmTime = 0f;
    private float rightArmTime = 0f;
    
    private BodyPhase leftPhase = BodyPhase.Idle;
    private BodyPhase rightPhase = BodyPhase.Idle;

    public enum BodyPhase
    {
        Idle,
        MovingArm,
        MovingBody
    }
    void Awake()
    {
        leftArmStart = LeftArm.connectedAnchor;
        rightArmStart = RightArm.connectedAnchor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*

        var leftMotor = LeftJoint.motor;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftMotor.motorSpeed -= Acceleration;
            LeftJoint.motor = leftMotor;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            leftMotor.motorSpeed += Acceleration;
            LeftJoint.motor = leftMotor;
        }


        //var leftOffset = LeftArm.connectedAnchor;
        if (Input.GetKeyDown(KeyCode.UpArrow) && leftPhase == BodyPhase.Idle)
        {
            leftPhase = BodyPhase.MovingArm;
            //LeftArm.connectedAnchor = leftOffset + new Vector2(0, 2f);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) && rightPhase == BodyPhase.Idle)
        {
            rightPhase = BodyPhase.MovingArm;
            //LeftArm.connectedAnchor = leftOffset + new Vector2(0, 2f);
        }
        
        */
        
        /*

        var newTime = time + Time.fixedDeltaTime;
        var leftOffset = 1f - Mathf.Abs(Mathf.Cos(newTime));
        if (newTime > 100f)
        {
            newTime -= 100f;
        }

        time = newTime;

        LeftArm.connectedAnchor = leftArmStart + new Vector2(0, leftOffset);
        
        */
        /*
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            leftOffset.y += 0.1f;
            leftOffset.y = Mathf.Clamp(leftOffset.y, -3f, -0.5f);
            LeftArm.linearOffset = leftOffset;
        }

        leftOffset.y = -Math.Abs(leftMotor.motorSpeed)/100f;
        leftOffset.y = Mathf.Clamp(leftOffset.y, -3f, -0.5f);
        LeftArm.linearOffset = leftOffset;
        
        */

        var leftX = Gamepad.current.leftStick.x.ReadValue();
        var leftY = Gamepad.current.leftStick.y.ReadValue();

        var angleLeft = Mathf.Atan2(leftY, leftX) * Mathf.Rad2Deg;
        //LeftJoint.connectedBody.rotation = angleLeft + 270f;
        
        var rightX = Gamepad.current.rightStick.x.ReadValue();
        var rightY = Gamepad.current.rightStick.y.ReadValue();
        
        var angleRight = Mathf.Atan2(rightY, rightX) * Mathf.Rad2Deg;
        //RightJoint.connectedBody.rotation = angleRight + 270;
        
        if (Gamepad.current.leftTrigger.isPressed && leftPhase == BodyPhase.Idle)
        {
            leftPhase = BodyPhase.MovingArm;
        }

        if (Gamepad.current.rightTrigger.isPressed && rightPhase == BodyPhase.Idle)
        {
            rightPhase = BodyPhase.MovingArm;
        }

        var leftArmPhaseOffset = 0f;
        var rightArmPhaseOffset = 0f;

        var leftMotor = LeftJoint.motor;
        leftMotor.motorSpeed -= Acceleration * leftX;
        LeftJoint.motor = leftMotor;
        
        var rightMotor = RightJoint.motor;
        rightMotor.motorSpeed -= Acceleration * rightX;
        RightJoint.motor = rightMotor;

        leftMotor.motorSpeed = Mathf.Clamp(leftMotor.motorSpeed, -10f, 10f);
        rightMotor.motorSpeed = Mathf.Clamp(rightMotor.motorSpeed, -10f, 10f);

        switch (leftPhase)
        {
            case BodyPhase.MovingArm:
                leftArmTime += Time.fixedDeltaTime * 3;
                leftArmPhaseOffset = Mathf.Abs(Mathf.Sin(leftArmTime));
                LeftArm.connectedAnchor = leftArmStart + new Vector2(0f,leftArmPhaseOffset)*3;
                if (leftArmTime >= Mathf.PI)
                {
                    LeftArm.connectedAnchor = leftArmStart;
                    leftArmTime = 0;
                    leftPhase = BodyPhase.Idle;
                    var direction = (LeftHandJoint.attachedRigidbody.position - LeftJoint.connectedBody.position).normalized;
                    Body.velocity = direction * 20;
                }
                break;
        }
        
        switch (rightPhase)
        {
            case BodyPhase.MovingArm:
                rightArmTime += Time.fixedDeltaTime * 3;
                rightArmPhaseOffset = Mathf.Abs(Mathf.Sin(rightArmTime));
                RightArm.connectedAnchor = rightArmStart + new Vector2(0f,rightArmPhaseOffset)*3;
                if (rightArmTime >= Mathf.PI)
                {
                    RightArm.connectedAnchor = rightArmStart;
                    rightArmTime = 0;
                    rightPhase = BodyPhase.Idle;
                    var direction = (RightHandJoint.attachedRigidbody.position - RightJoint.attachedRigidbody.position).normalized;
                    Body.velocity = direction * 20;
                }
                break;
        }
    }
}
