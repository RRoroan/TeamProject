using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private StatHandler statHandler;

    private void Awake()
    {
        Instance = this;
    }

    public StatHandler GetStatHandler()
    {
        return statHandler;
    }

}
