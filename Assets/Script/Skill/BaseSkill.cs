using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public string SkillName;
    public string RequiredItem;

    protected StatHandler statHandler;

    public void Awake()
    {
        statHandler = GameManager.Instance.GetStatHandler();
    }

    public abstract void UseSkill();
}
