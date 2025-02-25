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

}
