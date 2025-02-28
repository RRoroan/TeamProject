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
    public GameObject playerCharacter;
    public GameObject hpBar;


    private void Awake()
    {
        playerCharacter = GameManager.Instance.playerCharacter;
        hpBar = playerCharacter.GetComponentInChildren<HpBarController>(true).gameObject;
    }

    void Start()
    {
        GameManager.Instance.resourceController.CurrentHealth = GameManager.Instance.resourceController.MaxHealth;
        hpBar.SetActive(true);
        playerCharacter.transform.localScale = Vector3.one * 0.5f;
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
