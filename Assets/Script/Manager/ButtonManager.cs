using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject stageSelecter;
    public void StageSelecterOn()
    {
        stageSelecter.gameObject.SetActive(true);
    }
    public void StageSelecterOff()
    {
        stageSelecter.gameObject.SetActive(false);
    }
    public void GoSample()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void TitleButton()
    {
        SceneManager.LoadScene("Title");
    }
    public void CharacterSelecterButton()
    {
        SceneManager.LoadScene("CharacterSelecter");
    }
    public void OptionButton()
    {

    }
    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void Chalinge()
    {
        SceneManager.LoadScene("Chalinge");
    }

}
