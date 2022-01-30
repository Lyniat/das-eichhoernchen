using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BeeCollider : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log("COLLISION");
        if (col.gameObject.CompareTag("Squirrel"))
        {
            GameStateObserver.getInstance().OnHit();
        }
    }
}
