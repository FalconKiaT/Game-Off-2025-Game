using UnityEngine;

public class FullScreenButton : MonoBehaviour
{
    public void GoFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
