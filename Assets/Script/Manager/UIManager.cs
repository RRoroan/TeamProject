using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text waveCountText;
    public Text bossWarning;
    public Text bossWarningTop;
    public Text bossWarningBottom;
    public Image bossWarningBoard;
    public GameObject bossWarningObject;
    public int waveCount;

    private void Awake()
    {
        waveCountText = GameObject.Find("WaveCount").GetComponent<Text>();
    }

    public void UpWaveCount()
    {
        waveCount = StageManager.currentWaveIndex;
        waveCountText.text = waveCount.ToString();
    }
    public IEnumerator BossWaveWarning()
    { 
        bossWarningObject.SetActive(true);

        yield return new WaitForSeconds(4);

        bossWarningObject.SetActive(false);
    }
}
