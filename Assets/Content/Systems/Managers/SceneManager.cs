using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static SceneManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }

    public void LoadLevel(int _LevelID) => UnityEngine.SceneManagement.SceneManager.LoadScene(_LevelID);

    public void LoadLevel(string _LevelName) => UnityEngine.SceneManagement.SceneManager.LoadScene(_LevelName);

    public void ExitApplication() => Application.Quit();
}
