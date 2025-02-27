using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public MapSizeDetecte mapSize { get; private set; }

    private EnemyManager enemyManager;
    public static bool isFirstLoading = true;

    [SerializeField] private int currentWaveIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        mapSize = FindObjectOfType<MapSizeDetecte>();

        //statHandler = GetComponent<StatHandler>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(StageManager.Instance);
    }

    private void Start()
    {
        // ���� �Ŵ������� ĳ���� ������Ʈ�� ������
        GameObject player = GameManager.Instance.playerCharacter;

        // ������ ĳ���Ϳ� ���� �߰� �۾��� �� �� �ֽ��ϴ�
        // ��: ĳ������ ��ġ�� ���������� �°� ����
        if (player != null)
        {
            // ����: ĳ������ ��ġ�� ����
            player.transform.position = new Vector3(0, 0, 0);
        }

        if (isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        //uiManager.ChangeWave(currentWaveIndex);
        enemyManager.StartWave(1 + currentWaveIndex * 2);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
        //uiManager.SetGameOver();
    }
}
