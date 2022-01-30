using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current.aButton.isPressed)
        {
            SceneManager.LoadScene(1);
        }
        else if (Gamepad.current.bButton.isPressed)
        {
            Application.Quit();
        }
    }
}
