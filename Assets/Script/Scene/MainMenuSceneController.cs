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

    public GameObject blurImage;

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
        blurImage.SetActive(true);
    }

    void OnExitClicked()
    {
        SceneController.Instance.ExitGame();
    }
}
