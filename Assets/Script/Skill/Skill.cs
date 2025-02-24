using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public string skillName;
    public float cooldown;
    protected float lastUseTime;

    public abstract void Activate();
    public void UpdateSkill()
    {
        if (Time.time - lastUseTime >= cooldown)
        {
            Activate();
            lastUseTime = Time.time;
        }
    }
}
