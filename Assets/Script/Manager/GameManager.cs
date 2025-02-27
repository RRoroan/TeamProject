using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }
    public MapSizeDetecte mapSize { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private StatHandler statHandler;

    private EnemyManager enemyManager;
    public static bool isFirstLoading = true;

    [SerializeField] private int currentWaveIndex = 0;

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
        if (currentWaveIndex < 5)
        {
            currentWaveIndex += 1;

            enemyManager.StartWave(1 + currentWaveIndex * 2);
        }
        else if (currentWaveIndex == 5)
        {
            SceneController.Instance.BossStage(1, "Boss");
        }
        else
        {
            Debug.Log("다음 웨이브로 넘어갈 수 없습니다.");
        }
        


    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        SceneController.Instance.StartGame();

    }
}
