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
        player = FindObjectOfType<Player>();
    }

    protected virtual void Start()
    {
        statHandler = GameManager.Instance.GetStatHandler();
    }

    public abstract void UseSkill();
}
