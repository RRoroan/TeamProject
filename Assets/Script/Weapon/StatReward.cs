using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStatReward", menuName = "Rewards/Stat")]
public class StatReward : Reward
{
    public string statName;
    public float statIncrease;

    public override void ApplyReward(RangeWeaponHandler weaponHandler)
    {
        if (weaponHandler == null) return;

        switch (statName)
        {
            case "Bounce":
                weaponHandler.MaxBounces += (int)statIncrease;
                break;
            case "AttackSpeed":
                weaponHandler.Delay -= statIncrease;
                break;
            case "NumberOfProjectiles":
                weaponHandler.NumberoProjectilesPerShot += (int)statIncrease;
                break;
        }

        Debug.Log($"{rewardName} Àû¿ë: {statName} +{statIncrease}");
    }
}
