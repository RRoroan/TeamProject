using UnityEngine;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    void OnStartClicked()
    {
        SceneController.Instance.StartGame();
    }

    void OnExitClicked()
    {
        SceneController.Instance.ExitGame(); 
    }
}
