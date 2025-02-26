using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileItem : Item
{
    private RangeWeaponHandler rangeWeapon;

    protected override void Start()
    {
        base.Start();
        rangeWeapon = player.GetComponentInChildren<RangeWeaponHandler>();
    }

    public void AddProjectile()
    {
        rangeWeapon.NumberoProjectilesPerShot++;

    }

    public void IncreaseBullet()
    {
        rangeWeapon.BulletSize += 0.5f;
    }
}
