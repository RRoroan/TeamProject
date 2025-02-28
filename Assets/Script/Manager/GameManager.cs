using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }
    public ResourceController resourceController;

    public GameObject characterPrefab;
    public GameObject playerCharacter;

    private StatHandler statHandler;

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

        resourceController = player.GetComponent<ResourceController>();

        statHandler = player.GetComponent<StatHandler>();
    }
}
    
