using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour, IGameState
    {
        private bool startedGameOver;
        void Awake()
        {
            GameStateObserver.getInstance().Add(this);
        }
        
        public void OnReady()
        {
            
        }

        public void OnActive()
        {
            
        }

        public void OnHit()
        {
            
        }

        public void OnGameOver()
        {
            if (startedGameOver) return;
            startedGameOver = true;
            StartCoroutine(WaitForEnd());
        }

        private IEnumerator WaitForEnd()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(1);
        }
    }
}