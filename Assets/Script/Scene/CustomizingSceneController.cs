using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizingSceneController : MonoBehaviour
{
    public Button nextButton;
    public Button backButton;

    public WeaponChanger weaponChanger;
    public GameObject PlayerCharacter;
    public GameObject hpBar;

    private void Awake()
    {
        weaponChanger = GameManager.Instance.playerCharacter.GetComponent<WeaponChanger>();
        PlayerCharacter = GameManager.Instance.playerCharacter;
        hpBar = PlayerCharacter.GetComponentInChildren<HpBarController>().gameObject;
    }

    void Start()
    {
        nextButton.onClick.AddListener(ChangeWeapon);
        backButton.onClick.AddListener(GoToMainMenu);

        PlayerCharacter.transform.position = Vector3.zero;
        PlayerCharacter.transform.localScale = Vector3.one * 2f;
        hpBar.SetActive(false);
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
