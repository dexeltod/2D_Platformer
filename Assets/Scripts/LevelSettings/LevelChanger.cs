using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public void loadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}

