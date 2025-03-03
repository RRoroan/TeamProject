using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;

    [Header("EnemyList")]

    [SerializeField]
    private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트
    [SerializeField]
    private GameObject bossPrefab; //생성할 보스

    [Header("EnemySpawnArea")]

    [SerializeField]
    private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    public List<EnemyController> activeEnemies = new List<EnemyController>(); // 현재 활성화된 적들
    public EnemyController activeBoss = new EnemyController(); // 활성화된 보스

    private bool enemySpawnComplite;

    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    GameManager gameManager;
    StageManager stageManager;
 

    private void Start()
    {
        //StartWave(4);
    }

    public void Init(StageManager stageManager)
    {
        this.stageManager = stageManager;
    }

    public void StartWave(int waveCount)
    {
        if (waveCount <= 0)
        {
            stageManager.EndOfWave();
            return;
        }

        if(waveRoutine != null)
        {
            StopCoroutine(waveRoutine);
        }

        waveRoutine = StartCoroutine(SpawnWave(waveCount));
    }

    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount)
    {
        enemySpawnComplite = false;
        yield return new WaitForSeconds(timeBetweenWaves);

        if (StageManager.currentWaveIndex == 5)
        {
            yield return new WaitForSeconds(4);
        }
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnRandomEnemy();
        }
        //보스 소환
        SpawnBoss();

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤한 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

        // 랜덤한 영역 선택
        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();
        enemyController.Init(this, FindObjectOfType<PlayerController>().gameObject.transform);

        activeEnemies.Add(enemyController);
    }

    private void SpawnBoss()
    {
        if (bossPrefab == null)
            return;

        //보스 생성 및 변수에 추가
        GameObject spawnedBoss = Instantiate(bossPrefab, new Vector3(0, 2.5f), Quaternion.identity);
        EnemyController bossController = spawnedBoss.GetComponent<EnemyController>();
        bossController.Init(this, FindObjectOfType<PlayerController>().gameObject.transform);

        activeBoss = bossController;
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
        {
            Debug.Log("다음 웨이브");
            stageManager.EndOfWave();
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }
}
