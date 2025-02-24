using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10; // ü��
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    [Range(1f, 50f)][SerializeField] private float range = 5; // ��Ÿ�
    public float Range
    {
        get => range;
        set => range = Mathf.Clamp(value, 0, 50);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3; // �̵��ӵ�
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }

    [Range(0.1f, 10f)][SerializeField] private float attackspeed = 1; // ���� �ӵ�(������ ������ �ǹ�)
    public float AttackSpeed
    {
        get => attackspeed;
        set => attackspeed = Mathf.Clamp(value, 0.1f, 10);
    }

    [Range(0, 15)][SerializeField] private int armor = 0; // ����
    public int Armor
    {
        get => armor;
        set => armor = Mathf.Clamp(value, 0, 15);
    }

    [Range(1, 20)][SerializeField] private int projectileCount = 1; // ����ü ��
    public int ProjectileCount
    {
        get => projectileCount;
        set => projectileCount = Mathf.Clamp(value, 0, 20);
    }
}
