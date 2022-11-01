using UnityEngine;

public class ShopGameStopper : MonoBehaviour
{
    private void OnEnable()
    {
        int pauseTime = 0;
        Time.timeScale = pauseTime;
    }

    private void OnDisable()
    {
        int normalTime = 1;
        Time.timeScale = normalTime;
    }
}
