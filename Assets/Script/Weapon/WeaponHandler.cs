using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    public string weaponName;
    public float damage;
    public float attackSpeed;

    public abstract void Attack();
}
