using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingSceneController : MonoBehaviour
{
    public Button nextButton;
    public Button backButton;

    public WeaponChanger weaponChanger;

    void Start()
    {
        nextButton.onClick.AddListener(ChangeWeapon);
        backButton.onClick.AddListener(GoToMainMenu);
    }

    void ChangeWeapon()
    {
        weaponChanger.NextOption();
    }

    void GoToMainMenu()
    {
        SceneController.Instance.StartGame();
    }
}
