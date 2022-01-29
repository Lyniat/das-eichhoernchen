using System;
using UnityEngine;

public class CustomGizmo : MonoBehaviour
{

    public enum CustomGizmos
    {
        Joint
    }

    public CustomGizmos customGizmo;

    #if  UNITY_EDITOR
    private void OnDrawGizmos()
    {
        string name = "" + customGizmo + ".png";
        Gizmos.DrawIcon(transform.position,name,true);
    }
    #endif
}
