using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    public MapSizeDetecte mapSize { get; private set; }

    private EnemyManager enemyManager;
    private RewardManager rewardManager;
    private SkillManager skillManager;
    public static bool isFirstLoading = true;

    [SerializeField] public static int currentWaveIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        mapSize = FindObjectOfType<MapSizeDetecte>();

        //statHandler = GetComponent<StatHandler>();

        skillManager = FindObjectOfType<SkillManager>();
        rewardManager = GetComponentInChildren<RewardManager>();
        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(StageManager.Instance);
    }

    private void Start()
    {
        // 게임 매니저에서 캐릭터 오브젝트를 가져옴
        GameObject player = GameManager.Instance.playerCharacter;

        // 가져온 캐릭터에 대해 추가 작업을 할 수 있습니다
        // 예: 캐릭터의 위치를 스테이지에 맞게 설정
        if (player != null && currentWaveIndex < 5)
        {
            // 예시: 캐릭터의 위치를 초기화
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
            rewardManager.ShowRewards();
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
        enemyManager.activeEnemies = null;
        enemyManager.activeBoss = null;
        skillManager.ResetSkill();
        SceneController.Instance.StartGame();
    }
}