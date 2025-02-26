using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Reward : ScriptableObject
{
    public string rewardName;
    public string description;
    public abstract void ApplyReward(RangeWeaponHandler weaponHandler);
}
