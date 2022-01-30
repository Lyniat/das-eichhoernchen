using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour, IGameState
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
    [SerializeField]
    private float Movement = 0f;

    private float leftArmSpeed;
    private bool leftArmMoving = false;

    private float time = 0f;

    private Vector2 leftArmStart;
    private Vector2 rightArmStart;

    private float leftArmTime = 0f;
    private float rightArmTime = 0f;
    
    private BodyPhase leftPhase = BodyPhase.Idle;
    private BodyPhase rightPhase = BodyPhase.Idle;

    private float rightDirection = 0;
    private float leftDirection = 0;

    private CharacterState state = CharacterState.Moving;

    private Rigidbody2D rigidbody2D;
    

    public enum BodyPhase
    {
        Idle,
        MovingArm,
        MovingBody
    }

    public enum CharacterState
    {
        Moving,
        Hit,
        Dead
    }
    void Awake()
    {
        leftArmStart = LeftArm.connectedAnchor;
        rightArmStart = RightArm.connectedAnchor;
        rigidbody2D = GetComponent<Rigidbody2D>();
        GameStateObserver.getInstance().Add(this);
    }

    void MoveCharacter()
    {
        var leftX = Gamepad.current.leftStick.x.ReadValue();
        var leftY = Gamepad.current.leftStick.y.ReadValue();

        var usesLeft = Mathf.Abs(leftX) > 0.5f || Mathf.Abs(leftY) > 0.5f;

        leftDirection = usesLeft? Mathf.Sign(leftX) * Acceleration : 0f;

        var rightX = Gamepad.current.rightStick.x.ReadValue();
        var rightY = Gamepad.current.rightStick.y.ReadValue();

        var usesRight = Mathf.Abs(rightX) > 0.5f || Mathf.Abs(rightY) > 0.5f;
        
        rightDirection = usesRight? Mathf.Sign(rightX) * Acceleration : 0f;

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
        leftMotor.motorSpeed = leftDirection;
        LeftJoint.motor = leftMotor;
        
        var rightMotor = RightJoint.motor;
        rightMotor.motorSpeed = rightDirection;
        RightJoint.motor = rightMotor;
        
        switch (leftPhase)
        {
            case BodyPhase.MovingArm:
                leftArmTime += Time.fixedDeltaTime * 3;
                leftArmPhaseOffset = Mathf.Abs(Mathf.Sin(leftArmTime));
                LeftArm.connectedAnchor = leftArmStart + new Vector2(0f,leftArmPhaseOffset)*4;
                if (leftArmTime >= Mathf.PI/2f)
                {
                    leftPhase = BodyPhase.MovingBody;
                }
                break;
            case BodyPhase.MovingBody:
                leftArmTime += Time.fixedDeltaTime * 3;
                leftArmPhaseOffset = Mathf.Abs(Mathf.Sin(leftArmTime));
                LeftArm.connectedAnchor = leftArmStart + new Vector2(0f,leftArmPhaseOffset)*4;
                if (leftArmTime >= Mathf.PI)
                {
                    LeftArm.connectedAnchor = leftArmStart;
                    leftArmTime = 0;
                    leftPhase = BodyPhase.Idle;
                }
                break;
        }
        
        switch (rightPhase)
        {
            case BodyPhase.MovingArm:
                rightArmTime += Time.fixedDeltaTime * 3;
                rightArmPhaseOffset = Mathf.Abs(Mathf.Sin(rightArmTime));
                RightArm.connectedAnchor = rightArmStart + new Vector2(0f,rightArmPhaseOffset)*4;
                if (rightArmTime >= Mathf.PI/2f)
                {
                    rightPhase = BodyPhase.MovingBody;
                }
                break;
            case BodyPhase.MovingBody:
                rightArmTime += Time.fixedDeltaTime * 3;
                rightArmPhaseOffset = Mathf.Abs(Mathf.Sin(rightArmTime));
                RightArm.connectedAnchor = rightArmStart + new Vector2(0f,rightArmPhaseOffset)*4;
                if (rightArmTime >= Mathf.PI)
                {
                    RightArm.connectedAnchor = rightArmStart;
                    rightArmTime = 0;
                    rightPhase = BodyPhase.Idle;
                }
                break;
        }

        if (Body.velocity.y > 5)
        {
            var v = Body.velocity;
            v.y = 5;
            Body.velocity = v;
        }
    }

    private void Hit()
    {
        rigidbody2D.velocity = Vector2.down * 3;
        state = CharacterState.Dead;
    }

    private void Die()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (state)
        {
            case CharacterState.Moving:
                MoveCharacter();
                break;
            case CharacterState.Hit:
                Hit();
                break;
            case CharacterState.Dead:
                Die();
                break;
        }
        
    }

    public void OnReady()
    {
        throw new NotImplementedException();
    }

    public void OnActive()
    {
        throw new NotImplementedException();
    }

    public void OnHit()
    {
        Debug.Log("HIT");
        state = CharacterState.Dead;
    }

    public void OnGameOver()
    {
        throw new NotImplementedException();
    }

    public void OnHandEnter(Hand.HandType type)
    {
        if (type == Hand.HandType.Left)
        {
            if (leftPhase == BodyPhase.MovingArm)
            {
                leftPhase = BodyPhase.MovingBody;
                var direction = (LeftHandJoint.attachedRigidbody.position - LeftJoint.connectedBody.position).normalized;
                Body.velocity /= 4f;
                Body.velocity += direction * Movement;
            }
        }
        else
        {
            if (rightPhase == BodyPhase.MovingArm)
            {
                rightPhase = BodyPhase.MovingBody;
                var direction = (RightHandJoint.attachedRigidbody.position - RightJoint.connectedBody.position).normalized;
                Body.velocity /= 4f;
                Body.velocity += direction * Movement;
            }
        }
    }
}
