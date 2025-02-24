using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10; // 체력
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    [Range(1f, 50f)][SerializeField] private float range = 5; // 사거리
    public float Range
    {
        get => range;
        set => range = Mathf.Clamp(value, 0, 50);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3; // 이동속도
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }

    [Range(0.1f, 10f)][SerializeField] private float attackspeed = 1; // 공격 속도(실제로 쓸지는 의문)
    public float AttackSpeed
    {
        get => attackspeed;
        set => attackspeed = Mathf.Clamp(value, 0.1f, 10);
    }

    [Range(0, 15)][SerializeField] private int armor = 0; // 방어력
    public int Armor
    {
        get => armor;
        set => armor = Mathf.Clamp(value, 0, 15);
    }

    [Range(1, 20)][SerializeField] private int projectileCount = 1; // 투사체 수
    public int ProjectileCount
    {
        get => projectileCount;
        set => projectileCount = Mathf.Clamp(value, 0, 20);
    }
}
