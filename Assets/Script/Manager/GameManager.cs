using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    public GameObject characterPrefab;
    public GameObject playerCharacter;

    [SerializeField] private StatHandler statHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public StatHandler GetStatHandler()
    {
        return statHandler;
    }

    private void Start()
    {
        {
            if (playerCharacter == null)
            {
                playerCharacter = Instantiate(characterPrefab); 
                DontDestroyOnLoad(playerCharacter); 
            }
        }

        player = playerCharacter.GetComponent<PlayerController>();
        player.Init(this);

        _playerResourceController = player.GetComponent<ResourceController>();
    }
}
    
