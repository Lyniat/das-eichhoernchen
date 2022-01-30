using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //[SerializeField]
    //Canvas curtain;
    
    // Update is called once per frame
    //void Update()
    //{
    //    if (Gamepad.current.aButton.isPressed)
    //    {
    //        SceneManager.LoadScene(1);
    //    }
    //    else if (Gamepad.current.bButton.isPressed)
    //    {
    //        Application.Quit();
    //    }
    //}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        //Canvas curtain = GetComponent<Canvas>();
    }
}
