using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Camera : MonoBehaviour, IGameState
{
    [SerializeField] private Transform target;
    // Start is called before the first frame update

    private CameraState state = CameraState.Follow;
    
    public enum CameraState
    {
        Follow,
        Idle
    }
    
    void Start()
    {
        GameStateObserver.getInstance().Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CameraState.Follow:
                transform.position = new Vector3(target.position.x, target.position.y,-10);
                break;
            case CameraState.Idle:
                break;
        }
    }

    public void OnReady()
    {
        throw new System.NotImplementedException();
    }

    public void OnActive()
    {
        throw new System.NotImplementedException();
    }

    public void OnHit()
    {
        state = CameraState.Idle;
    }

    public void OnGameOver()
    {
        throw new System.NotImplementedException();
    }
}
