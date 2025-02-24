using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] float buleetSize = 1f;
    public float BulletSize { get => buleetSize; }

    [SerializeField] private float duration;
    public float Duration { get => duration; }

    protected override void Attack()
    {
        base.Attack();

        
    }

}
