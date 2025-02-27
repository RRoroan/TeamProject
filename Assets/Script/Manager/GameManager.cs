using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }
    public MapSizeDetecte mapSize { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private StatHandler statHandler;

    private EnemyManager enemyManager;
    public static bool isFirstLoading = true;

    [SerializeField] public static int currentWaveIndex = 0;

    private void Awake()
    {
        Instance = this;

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        mapSize = FindObjectOfType<MapSizeDetecte>();

        //statHandler = GetComponent<StatHandler>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        _playerResourceController = player.GetComponent<ResourceController>();
    }

    public StatHandler GetStatHandler()
    {
        return statHandler;
    }

    private void Start()
    {
        if (isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = true;
        }
    }

    public void StartGame()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        Debug.Log($"Index : {currentWaveIndex}");
        enemyManager.StartWave(1 + currentWaveIndex * 2);

    }

    public void EndOfWave()
    {
        currentWaveIndex += 1;

        if (currentWaveIndex < 5)
        {
            StartNextWave();
        }
        else if (currentWaveIndex == 5)
        {
            SceneController.Instance.BossStage(1, "Boss");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
        
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        SceneController.Instance.StartGame();

    }
}
