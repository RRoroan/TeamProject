using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public float cooldown;
    protected float useTime;

    protected StatHandler statHandler = GameManager.Instance.GetStatHandler();
    public abstract void UseSkill(StatHandler handler);
}
