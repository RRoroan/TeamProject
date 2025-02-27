using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour

{
    public Slider volumeSlider;  
    public Button speakerButton;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        volumeSlider.value = AudioListener.volume;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        speakerButton.onClick.AddListener(SliderOnOff);
    }

    public void SliderOnOff()
    {
        GameObject go = volumeSlider.gameObject;

        go.SetActive(!go.activeSelf);
        Debug.Log("SliderOnOff");
    }

    private void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
