using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Reward : ScriptableObject
{
    public string rewardName;
    public string description;
    public virtual void ApplyReward(RangeWeaponHandler weaponHandler) { }
    public virtual void ApplyReward(SkillManager skillManager) { }

}
