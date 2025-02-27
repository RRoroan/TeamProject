using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance {  get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void Stage(int stageNum, int subNum)
    {
        SceneManager.LoadScene($"Stage{stageNum.ToString()}_{subNum.ToString()}");
    }

    public void Customizing()
    {
        SceneManager.LoadScene("Customizing");
    }
}
