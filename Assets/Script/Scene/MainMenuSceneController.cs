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
    public Button closeOptionButton;

    public GameObject optionPanel;

    void Start()
    {
        GameManager.Instance.resourceController.CurrentHealth = GameManager.Instance.resourceController.MaxHealth;
        stageSelectButton.onClick.AddListener(OnStageSelectClicked);
        customizingButton.onClick.AddListener(OnCustomizingClicked);
        optionButton.onClick.AddListener(OnOptionClicked);
        exitButton.onClick.AddListener(OnExitClicked);
        closeOptionButton.onClick.AddListener(OnCloseOptionClicked);
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
        optionPanel.SetActive(true);
    }

    void OnExitClicked()
    {
        SceneController.Instance.ExitGame();
    }

    void OnCloseOptionClicked()
    {
        optionPanel.SetActive(false);
    }
}
