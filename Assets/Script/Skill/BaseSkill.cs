using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string SkillName;
    public string RequiredItem;

    protected StatHandler statHandler;
    protected Player player;

    public void Awake()
    {
        statHandler = GameManager.Instance.GetStatHandler();
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        
    }

    public abstract void UseSkill();
}
