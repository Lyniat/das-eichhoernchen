using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public enum HandType
    {
        Left,
        Right
    }
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private HandType handType;
    private void OnTriggerEnter2D(Collider2D col)
    {
        characterController.OnHandEnter(handType);
    }
}
