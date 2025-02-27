using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneController : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button backButton;

    void Start()
    {
        stage1Button.onClick.AddListener(() => OnStageClicked(1));
        stage2Button.onClick.AddListener(() => OnStageClicked(2));
        backButton.onClick.AddListener(GoToMainMenu);
    }

    void OnStageClicked(int stageNum)
    {
        StageManager.currentWaveIndex = 0;
        SceneController.Instance.Stage(stageNum, 1);
    }

    void GoToMainMenu()
    {
        SceneController.Instance.StartGame();
    }
}

