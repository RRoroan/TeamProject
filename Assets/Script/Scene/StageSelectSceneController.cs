using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneController : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;

    void Start()
    {
        stage1Button.onClick.AddListener(() => OnStageClicked(1));
        stage2Button.onClick.AddListener(() => OnStageClicked(2));
    }

    void OnStageClicked(int stageNum)
    {
        SceneController.Instance.Stage(stageNum, 1);
    }
}

