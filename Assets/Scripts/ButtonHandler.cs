using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void FullscreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void replay()
    {
        SceneManager.LoadScene("Main");
    }
}
