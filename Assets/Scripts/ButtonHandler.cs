using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    private void OnClick()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
