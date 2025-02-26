using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuSceneController : MonoBehaviour
{
    public Button stageSelectButton;
    public Button customizingButton;
    public Button optionButton;
    public Button exitButton;

    void Start()
    {
        stageSelectButton.onClick.AddListener(OnStageSelectClicked);
        customizingButton.onClick.AddListener(OnCustomizingClicked);
        optionButton.onClick.AddListener(OnOptionClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    void OnStageSelectClicked()
    {
        SceneController.Instance.StageSelect();
    }

    void OnCustomizingClicked()
    {
        SceneController.Instance.Customizing();
    }

    void OnOptionClicked()
    {

    }

    void OnExitClicked()
    {
        SceneController.Instance.ExitGame();
    }
}
