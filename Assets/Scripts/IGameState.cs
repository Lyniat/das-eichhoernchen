using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    void OnReady();
    void OnActive();
    void OnHit();
    void OnGameOver();
}
