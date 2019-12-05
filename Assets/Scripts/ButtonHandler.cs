using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void OnClick()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
